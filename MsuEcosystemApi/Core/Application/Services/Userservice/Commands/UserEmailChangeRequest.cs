using Domain.Entitties.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using NETCore.MailKit.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.UserService.Commands
{
    public static class UserEmailChangeRequest
    {
        public record Command(MsuUser User, string Email) : IRequest<Response>;

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
                var changeEmailToken = await _userManager.GenerateChangeEmailTokenAsync(request.User, request.Email);
                var code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(request.User.Id + '&' + request.Email + '&' + changeEmailToken));
                await _emailService.SendAsync(request.Email, "Изменение адреса электронной почты", $"{code}", true);
                return new Response(true, "Вам необходимо подтвердить новый адрес электронной почты");
            }
        }
    }
}
