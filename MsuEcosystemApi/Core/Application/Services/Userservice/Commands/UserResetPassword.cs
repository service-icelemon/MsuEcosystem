using Domain.Entitties.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using NETCore.MailKit.Core;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.UserService.Commands
{
    public static class UserResetPassword
    {
        public record Command(string Code, string Password) : IRequest<Response>;

        public record Response(bool Succeeded, string Message);

        public class Handler : IRequestHandler<Command, Response>
        {
            private readonly UserManager<MsuUser> _userManager;
            private readonly IEmailService _emailService;

            public Handler(UserManager<MsuUser> userManager, IEmailService emailService)
            {
                _userManager = userManager;
                _emailService = emailService;
            }

            public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
            {
                var decoded = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(request.Code));

                var user = await _userManager.FindByIdAsync(decoded.Substring(0, decoded.IndexOf('&')));
                var token = decoded[(decoded.IndexOf('&') + 1)..];

                var result = await _userManager.ResetPasswordAsync(user, token, request.Password);
                return result.Succeeded ?
                    new Response(true, $"Пароль изменён") :
                    new Response(false, "Что-то пошло не так");
            }
        }
    }
}
