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
    public static class UserAddRole
    {
        public record Command(string UserId, string RoleName) : IRequest<Response>;

        public record Response(bool Succeeded, string Message);

        public class Handler : IRequestHandler<Command, Response>
        {
            private readonly UserManager<MsuUser> _userManager;
            private readonly RoleManager<IdentityRole> _roleManager;

            public Handler(UserManager<MsuUser> userManager, RoleManager<IdentityRole> roleManager)
            {
                _userManager = userManager;
                _roleManager = roleManager;
            }

            public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
            {
                var user = await _userManager.FindByIdAsync(request.UserId);
                
                if (user == null)
                {
                    return new Response(false, "Такого пользователя не существует");
                }
                var role = await _roleManager.FindByNameAsync(request.RoleName);

                if (role == null)
                {
                    return new Response(false, "Такой роли не существует");
                }
                var result = await _userManager.AddToRoleAsync(user, role.ToString());
                return result.Succeeded ?  
                    new Response(true, $"Роль {role} успешно добавлена аккаунту {user.UserName}") :
                    new Response(false, "Что-то пошло не так");
            }
        }
    }
}
