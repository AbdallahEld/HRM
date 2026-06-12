using HR.Domain.Data.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace HR.API.Helper
{
    public static class AdminSeeder
    {
        public static async Task SeedAdminAsync(RoleManager<Role> roleManager, UserManager<User> userManager)
        {
            if (!await roleManager.RoleExistsAsync(SystemRoles.SystemAdmin))
            {
                var role = new Role
                {
                    Name = SystemRoles.SystemAdmin
                };
                await roleManager.CreateAsync(role);
            }

            var adminEmail = "abdallaheldesoky301@gmail.com";
            var admin = await userManager.FindByEmailAsync(adminEmail);

            if (admin is null)
            {
                var adminPassword = "P@ssw0rd";
                var newAdmin = new User
                {
                    UserName = "Remando",
                    Email = adminEmail,
                };
                await userManager.CreateAsync(newAdmin, adminPassword);
                await userManager.AddToRoleAsync(newAdmin, SystemRoles.SystemAdmin);
            }
        }

        public static async Task SeedRolesAsync (RoleManager<Role> roleManager)
        {
            var roles = new[] { SystemRoles.SystemAdmin, SystemRoles.HRManager, SystemRoles.Employee };
            foreach (var roleName in roles)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    var role = new Role
                    {
                        Name = roleName
                    };
                    await roleManager.CreateAsync(role);
                }
            }
        }
    }
}
