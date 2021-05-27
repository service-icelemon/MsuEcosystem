using Application.Services.InfoService.StudentFeatures.Queries;
using Application.Services.InfoService.TeacherFeatures.Queries;
using Domain.Entitties.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using NETCore.MailKit.Core;
using Persistence.Constants;
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
            private readonly IMediator _mediator;

            public Handler(UserManager<MsuUser> userManager, IEmailService emailService, IMediator mediator)
            {
                _userManager = userManager;
                _emailService = emailService;
                _mediator = mediator;
            }

            public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
            {
                if (!request.User.IsTeacher)
                {
                    var isValid = await _mediator.Send(new CheckStudent.Query(request.User.StudentCardId));
                    if (!isValid.Successed)
                    {
                        return new Response(false, isValid.Message);
                    }
                }
                else
                {
                    var isValid = await _mediator.Send(new CheckTeacher.Query(request.User.TeacherCode));
                    if (!isValid.Successed)
                    {
                        return new Response(false, isValid.Message);
                    }
                }
                var user = new MsuUser
                {
                    Email = request.User.Email,
                    UserName = request.User.UserName,
                    TeacherCode = request.User.TeacherCode,
                    StudentCardId = request.User.StudentCardId,
                    IsTeacher = request.User.IsTeacher
                };
                var result = await _userManager.CreateAsync(user, request.User.Password);
                if (result.Succeeded)
                {
                    var emailConfirmToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var encodedEmailToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(user.Id + '&' + emailConfirmToken));

                    await _userManager.AddToRoleAsync(user, Authorization.default_role.ToString());
                    await _emailService.SendAsync(user.Email, "Подтверждение регистрации на msu.by", $"<a href='http://localhost:3000/emailconfirmation/{encodedEmailToken}'>подтверждение электронной почты</a>", true);
                    return new Response(true, $"Для активации аккаунта необходимо подтвердить почту, указанную при регистрации");
                }
                return new Response(true, $"{result.Errors.First().Description}");
            }
        }
    }
}
