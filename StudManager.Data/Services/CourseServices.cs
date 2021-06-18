using StudManager.Data.Context;
using StudManager.Data.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StudManager.Data.Services
{
    public class CourseServices : ICourseService
    {
        private readonly DBContext _context;

        public CourseServices(DBContext context)
        {
            _context = context;
        }
        public void AddCourse(Course model)
        {
            _context.Courses.Add(model);
        }

        public IEnumerable<Course> GetAllCourse()
        {
            return _context.Courses.OrderBy(c => c.CourseId).ToList();
        }

        public bool SaveAll()
        {
            return _context.SaveChanges() > 0;
        }

        public void UpdateCourse(Course model)
        {
            var course = _context.Courses.FirstOrDefault(C => C.CourseId == model.CourseId);

            course = model;

            _context.SaveChanges();
        }
    }
}
