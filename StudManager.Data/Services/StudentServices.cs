using Microsoft.AspNetCore.Identity;
using StudManager.Data.Context;
using StudManager.Data.Data.Entities;
using StudManager.Data.Data.Roles;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace StudManager.Data.Services
{
    public class StudentServices : IStudentServices
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly DBContext _context;
        public StudentServices(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, DBContext context)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _context = context; 
        }

        public async Task<bool> CreateStudent(ApplicationUser user, string password)
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
                    await _userManager.AddToRoleAsync(user, UserRoles.User);
                }
            }

            return result.Succeeded;
            
        }


        public async Task<bool> ChangePassword(ApplicationUser user, string currentPassword, string newPassword)
        {
            var result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword); 

            return result.Succeeded;
        }

        public int UpdateStudent(ApplicationUser user)
        {
            var student = _context.Users.Include(u => u.Student).Where(s => s.UserType == "Student" && s.Id == user.Id).FirstOrDefault();

            student.Email = user.Email;
            student.FirstName = user.FirstName;
            student.LastName = user.LastName;
            student.PhoneNumber = user.PhoneNumber;
            student.BirthDate = user.BirthDate;
            student.UserName = user.UserName;
            student.Student.StudRegNo = user.Student.StudRegNo;
            student.Student.FullName = user.FirstName + " " + user.LastName;
            var result = _context.SaveChanges();

            return result;

        }

        public async Task<ApplicationUser> ExistUserByName(string userName)
        {
            var userExist = await _userManager.FindByNameAsync(userName);

            return userExist;
        }

        public async Task<ApplicationUser> ExistUserById(string id)
        {
            var userExist = await _userManager.FindByIdAsync(id);

            return userExist;
        }

        public List<ApplicationUser> GetAllStudents()
        {
            var students = _context.Users.Include(u => u.Student).Where(s => s.UserType == "Student").ToList();

            return students;
            
        }

        public async Task<ApplicationUser> GetStudent(string id)
        {
            var userExist = _context.Users.Include(u => u.Student).Where(s => s.UserType == "Student" && s.Id == id).FirstOrDefault();

            return userExist;
        }
    }
}
