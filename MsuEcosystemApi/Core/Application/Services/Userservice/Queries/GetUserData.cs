using Application.Services.InfoService.StudentFeatures.Queries;
using Application.Services.InfoService.TeacherFeatures.Queries;
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
            private readonly IMediator _mediator;

            public Handler(UserManager<MsuUser> userManager, IMediator mediator)
            {
                _userManager = userManager;
                _mediator = mediator;
            }

            public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
            {
                var userEmail = request.User.FindFirst(ClaimTypes.Email).Value;
                var user = await _userManager.FindByEmailAsync(userEmail);
                var userViewModel = new UserViewModel
                {
                    AccountId = user.Id,
                    Email = user.Email,
                    Roles = await _userManager.GetRolesAsync(user).ConfigureAwait(false),
                    IsTeacher = user.IsTeacher,
                };
                if (user.IsTeacher)
                {
                    userViewModel.TeacherData = await _mediator.Send(new GetTeacherByCode.Query(user.TeacherCode));
                }
                else
                {
                    userViewModel.StudentData = await _mediator.Send(new GetStudentByStudentCard.Query(user.StudentCardId));
                }
                return user != null ?
                    new Response(true, $"Успшено", userViewModel) :
                    new Response(false, "Что-то пошло не так", null);
            }
        }
    }
}
