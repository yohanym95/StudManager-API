using Microsoft.AspNetCore.Identity;
using StudManager.Data.Data.Entities;
using StudManager.Data.Data.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudManager.Data.Context
{
    public static class ContextSeed
    {
        public static async Task SeedRolesAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Roles
            await roleManager.CreateAsync(new IdentityRole(UserRoles.SuperAdmin));
            await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            await roleManager.CreateAsync(new IdentityRole(UserRoles.User));
        }

        public static async Task SeedSuperAdminAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Default User
            var defaultUser = new ApplicationUser
            {
                UserName = "superadmin",
                Email = "malshika@gmail.com",
                FirstName = "Yohan",
                LastName = "Malshika",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };
            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "123Pa$$word.");
                    await userManager.AddToRoleAsync(defaultUser, UserRoles.SuperAdmin);
                    await userManager.AddToRoleAsync(defaultUser, UserRoles.Admin);
                    await userManager.AddToRoleAsync(defaultUser, UserRoles.User);
                }

            }
        }
    }
}
