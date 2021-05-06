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
    public static class UserResetPasswordRequest
    {
        public record Command(string Email) : IRequest<Response>;

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
                var user = await _userManager.FindByEmailAsync(request.Email);
                if (user != null)
                {
                    var resetPasswordToken = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(user.Id + '&' + resetPasswordToken));
                    await _emailService.SendAsync(request.Email, "Смена пароля на msu.by", $"{code}", true);
                    return new Response(true, "Подтвердите почту");
                }
                return new Response(false, "Пользователя с таким адресом электронной почты не существует");
            }
        }
    }
}
