using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.UserService.Queries
{
    public static class GetRoleList
    {
        public record Query() : IRequest<IEnumerable<string>>;

        public class Handler : IRequestHandler<Query, IEnumerable<string>>
        {
            private readonly RoleManager<IdentityRole> _roleManager;

            public Handler(RoleManager<IdentityRole> roleManager)
            {
                _roleManager = roleManager;
            }

            public async Task<IEnumerable<string>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _roleManager.Roles.Select(i => i.Name).ToListAsync();
            }
        }
    }
}
