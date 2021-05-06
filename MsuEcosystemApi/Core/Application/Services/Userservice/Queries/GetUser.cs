using Domain.Entitties.Identity;
using Domain.Entitties.Identity.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.UserService.Queries
{
    public static class GetUser
    {
        public record Query(string id) : IRequest<Response>;

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
                var user = await _userManager.FindByIdAsync(request.id);
                if (user == null)
                {
                    return new Response(false, "Такого пользователя нет", null);
                }
                var userViewModel = new UserViewModel
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    FatherName = user.FatherName,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    StudentCardId = user.StudentCardId,
                    GroupNumber = user.GroupNumber,
                    FacultyId = user.FacultyId,
                    Email = user.Email,
                    Roles = await _userManager.GetRolesAsync(user)
                };
                return new Response(true, $"Успшено", userViewModel);
            }
        }
    }
}
