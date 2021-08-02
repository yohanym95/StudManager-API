using StudManager.Data.Data.Entities;
using StudManager.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StudManager.Data.Services
{
    public interface IStudentServices
    {
        Task<bool> CreateStudent(ApplicationUser user, string password);
        Task<ApplicationUser> ExistUser(string userName);
        Task<bool> UpdateStudent(ApplicationUser user);
        Task<bool> ChangePassword(ApplicationUser user, string currentPassword, string newPassword);
        List<ApplicationUser> GetAllStudents();
        Task<ApplicationUser> GetStudent(string id);
    }
}
