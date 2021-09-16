using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Persistence
{
    public static class ApplicationDbContextSeed
    {
        // Seed the db with the roles
        public static async Task SeedDefaultUserAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var roles = new List<IdentityRole> {
                IdentityRoles.SystemAdmin
            };

            foreach (var role in roles)
            {
                if (roleManager.Roles.All(r => r.Name != role.Name))
                {
                    await roleManager.CreateAsync(role);
                }
            }

            var administrator = new ApplicationUser
            {
                UserName = "SystemAdmin",
                Email = "systemAdmin@poneahealth.com" 
            };

            if (userManager.Users.All(u => u.UserName != administrator.UserName))
            {
                await userManager.CreateAsync(administrator, "systemAdmin@123!");
                await userManager.AddToRolesAsync(administrator, new [] { IdentityRoles.SystemAdmin.Name });
            }
        }
    }
}
