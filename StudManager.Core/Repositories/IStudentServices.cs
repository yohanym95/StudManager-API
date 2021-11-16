using StudManager.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StudManager.Core.Repositories
{
    public interface IStudentServices 
    {
        Task<bool> CreateStudent(ApplicationUser user, string password);
        Task<ApplicationUser> ExistUserByName(string userName);
        Task<ApplicationUser> ExistUserById(string id);
        int UpdateStudent(ApplicationUser user);
        Task<bool> ChangePassword(ApplicationUser user, string currentPassword, string newPassword);
        List<ApplicationUser> GetAllStudents();
        Task<ApplicationUser> GetStudent(string id);
        Task<Student> GetStudent(int id);
    }
}
