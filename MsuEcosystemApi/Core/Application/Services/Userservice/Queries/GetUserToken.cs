using Application.Interfaces;
using Application.Services.InfoService.StudentFeatures.Queries;
using Application.Services.InfoService.TeacherFeatures.Queries;
using Domain.Entitties.Identity;
using Domain.Entitties.Identity.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.UserService.Queries
{
    public static class GetUserToken
    {
        public record Query(string Email, string Password) : IRequest<Response>;

        public record Response(bool Succeeded, string Message, UserViewModel User, string AccessToken, string RefreshToken);

        public class Handler : IRequestHandler<Query, Response>
        {
            private readonly UserManager<MsuUser> _userManager;
            private readonly IJWTService _jwtService;
            private readonly IMediator _mediator;

            public Handler(UserManager<MsuUser> userManager, IJWTService jwtService, IMediator mediator)
            {
                _userManager = userManager;
                _jwtService = jwtService;
                _mediator = mediator;
            }

            public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
            {
                var user = await _userManager.FindByEmailAsync(request.Email);
                if (user == null)
                {
                    return new Response(false, "Такого пользователя не существует", null, null, null);
                }
                if (await _userManager.CheckPasswordAsync(user, request.Password))
                {
                    var tokens = await _jwtService.CreateJwtToken(user);
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
                    return new Response(true, "успешно", userViewModel, tokens.AccessToken, tokens.RefreshToken);
                }
                return new Response(false, "Неверный пароль", null, null, null);
            }
        }
    }
}
