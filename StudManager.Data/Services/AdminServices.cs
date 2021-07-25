﻿using Microsoft.AspNetCore.Identity;
using StudManager.Data.Data.Entities;
using StudManager.Data.Data.Roles;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StudManager.Data.Services
{
    public class AdminServices : IAdminService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminServices(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task<bool> CreateManagementUser(ApplicationUser user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
                    await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                if (!await _roleManager.RoleExistsAsync(UserRoles.User))
                    await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));

                if (user.UserType == "User" && await _roleManager.RoleExistsAsync(UserRoles.User))
                {
                    await _userManager.AddToRoleAsync(user, UserRoles.Admin);
                }
            }

            return result.Succeeded;

        }

        public async Task<ApplicationUser> ExistUser(string userName)
        {
            var userExist = await _userManager.FindByNameAsync(userName);

            return userExist;
        }

        public async Task<bool> UpdateManagementUser(ApplicationUser user)
        {
            var result = await _userManager.UpdateAsync(user);

            return result.Succeeded;

        }
    }
}
