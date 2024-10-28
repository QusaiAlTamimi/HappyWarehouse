using HappyWarehouseCore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace HappyWarehouseData.DataAccess
{
    public static class DbInitializer
    {
        public static async Task Seed(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            // Seed default roles if necessary (e.g., "Admin")
            string[] roleNames = { "Admin" ,"User"};
            foreach (var roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            // Seed default admin user
            if (await userManager.FindByEmailAsync("admin@happywarehouse.com") == null)
            {
                var user = new ApplicationUser
                {
                    FullName = "admin@happywarehouse.com",
                    UserName = "admin@happywarehouse.com",
                    Email = "admin@happywarehouse.com",
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(user, "P@ssw0rd");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Admin");
                }
            }
        }
    }
}
