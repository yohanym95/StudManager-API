using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using StudManager.Data.Context;
using StudManager.Data.Data.Entities;
using StudManager.Data.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StudManager.Data.Configuration
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DBContext _context;
        private readonly ILogger _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;


        public ICourseService Courses { get; private set; }
        public IFeesServices Fees { get; private set; }
        public IStudentServices Student { get; private set; }
        public  IAdminService Admin { get; private set; }

        public ISubjectService Subject { get; private set; }

        public UnitOfWork(DBContext context, ILoggerFactory loggerFactory, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger("logs");
            _userManager = userManager;
            _roleManager = roleManager;

            Courses = new CourseServices(context, _logger);
            Fees = new FeesServices(context, _logger);
            Subject = new SubjectService(context, _logger);
            Student = new StudentServices(_userManager, _roleManager, context, _logger);
            Admin = new AdminServices(_userManager, _roleManager, context, _logger);
       }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
