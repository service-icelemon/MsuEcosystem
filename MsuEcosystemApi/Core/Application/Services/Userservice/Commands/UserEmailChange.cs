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
    public static class UserEmailChange
    {
        public record Command(string Code) : IRequest<Response>;

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

                var data = decoded.Split('&');
                var user = await _userManager.FindByIdAsync(data[0]);
                var newEmail = data[1];
                var token = data[2];

                var result = await _userManager.ChangeEmailAsync(user, newEmail, token);
                return result.Succeeded ?
                    new Response(true, $"Адрес электронной почты успешно изменён") :
                    new Response(false, "Что-то пошло не так");
            }
        }
    }
}
