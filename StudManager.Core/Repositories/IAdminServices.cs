using StudManager.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StudManager.Core.Repositories
{
    public interface IAdminServices 
    {
        Task<bool> CreateManagementUser(ApplicationUser user, string password);
        Task<ApplicationUser> ExistUser(string userName);
        Task<bool> UpdateManagementUser(ApplicationUser user);
    }
}
