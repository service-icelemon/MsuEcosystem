using Domain.Entitties.Identity;
using Microsoft.AspNetCore.Identity;
using Persistence.Constants;
using System.Linq;
using System.Threading.Tasks;

namespace Persistence.Contexts
{
    public class MsuIdentityContextSeed
    {
        public static async Task SeedEssentialsAsync(UserManager<MsuUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            await roleManager.CreateAsync(new IdentityRole(Authorization.Roles.Administrator.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Authorization.Roles.Moderator.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Authorization.Roles.User.ToString()));

            var defaultUser = new MsuUser
            {
                UserName = Authorization.default_username,
                Email = Authorization.default_email,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };
            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                await userManager.CreateAsync(defaultUser, Authorization.default_password);
                await userManager.AddToRoleAsync(defaultUser, Authorization.default_role.ToString());
            }
        }
    }
}
