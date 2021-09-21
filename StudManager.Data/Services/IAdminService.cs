﻿using StudManager.Data.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StudManager.Data.Services
{
    public interface IAdminService
    {
        Task<bool> CreateManagementUser(ApplicationUser user, string password);
        Task<ApplicationUser> ExistUser(string userName);
        Task<bool> UpdateManagementUser(ApplicationUser user);
    }
}
