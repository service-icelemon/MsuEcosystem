using Domain.Entitties.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using NETCore.MailKit.Core;
using Persistence.Constants;
using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.UserService.Commands
{
    public static class RegisterUser
    {
        public record Command(UserRegisterModel User) : IRequest<Response>;

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
                var user = new MsuUser
                {
                    AvatarImage = request.User.AvatarImage,
                    Email = request.User.Email,
                    UserName = request.User.UserName,
                    FirstName = request.User.FirstName,
                    LastName = request.User.LastName,
                    FatherName = request.User.FatherName,
                    GroupNumber = Convert.ToInt32(request.User.GroupNumber),
                    StudentCardId = Convert.ToInt32(request.User.GroupNumber),
                    FacultyId = Convert.ToInt32(request.User.FacultyId),
                    IsTeacher = request.User.IsTeacher
                };
                var result = await _userManager.CreateAsync(user, request.User.Password);
                if (result.Succeeded)
                {
                    var emailConfirmToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var encodedEmailToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(user.Id + '&' + emailConfirmToken));

                    await _userManager.AddToRoleAsync(user, Authorization.default_role.ToString());
                    await _emailService.SendAsync(user.Email, "Подтверждение регистрации на msu.by", $"<h4>{encodedEmailToken}</h4>", true);
                    return new Response(true, $"Для активации аккаунта необходимо подтвердить почту, указанную при регистрации");
                }
                return new Response(true, $"{result.Errors.First().Description}");
            }
        }
    }
}
