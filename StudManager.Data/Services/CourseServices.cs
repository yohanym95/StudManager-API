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
            return _context.Courses.OrderBy(c => c.Id).ToList();
        }

        public Course GetCourse(int id)
        {
            return _context.Courses.Where(s => s.Id == id).FirstOrDefault();
        }

        public bool SaveAll()
        {
            return _context.SaveChanges() > 0;
        }

        public async void UpdateCourse(Course model)
        {
            var course = _context.Courses.FirstOrDefault(C => C.Id == model.Id);

            if (course == null) throw new Exception("Course record is not found");

            course.CourseName = model.CourseName;
            course.CourseNo = model.CourseNo;
            course.Qualifications = model.Qualifications;
            //_context.Update(course);
            await _context.SaveChangesAsync(); 
        }
    }
}
