using Domain.Entitties.Identity;
using Domain.Entitties.Identity.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.UserService.Queries
{
    public static class GetUserData
    {
        public record Query(ClaimsPrincipal User) : IRequest<Response>;

        public record Response(bool Succeeded, string Message, UserViewModel User);

        public class Handler : IRequestHandler<Query, Response>
        {
            private readonly UserManager<MsuUser> _userManager;

            public Handler(UserManager<MsuUser> userManager)
            {
                _userManager = userManager;
            }

            public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
            {
                var userEmail = request.User.FindFirst(ClaimTypes.Email).Value;
                var user = await _userManager.FindByEmailAsync(userEmail);
                var userViewModel = new UserViewModel
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    AvatarImage = user.AvatarImage,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    FatherName = user.FatherName,
                    FacultyId = user.FacultyId,
                    StudentCardId = user.StudentCardId,
                    GroupNumber = user.GroupNumber,
                    Roles = await _userManager.GetRolesAsync(user).ConfigureAwait(false),
                    IsTeacher = user.IsTeacher,
                };
                return user != null ?
                    new Response(true, $"Успшено", userViewModel) :
                    new Response(false, "Что-то пошло не так", null);
            }
        }
    }
}
