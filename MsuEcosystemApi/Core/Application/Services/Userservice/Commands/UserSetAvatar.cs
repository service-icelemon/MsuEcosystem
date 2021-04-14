using Domain.Entitties.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.UserService.Commands
{
    public static class UserSetAvatar
    {
        public record Command(MsuUser User, string AvatarLink) : IRequest<Response>;

        public record Response(bool Succeeded, string Message);

        public class Handler : IRequestHandler<Command, Response>
        {
            private readonly UserManager<MsuUser> _userManager;
            private readonly RoleManager<IdentityRole> _roleManager;

            public Handler(UserManager<MsuUser> userManager)
            {
                _userManager = userManager;
            }

            public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
            {
                request.User.AvatarImage = request.AvatarLink;
                var result = await _userManager.UpdateAsync(request.User);
                return result.Succeeded ?
                    new Response(true, $"Фото профиля успешно обновлено") :
                    new Response(false, "Что-то пошло не так");
            }
        }
    }
}
