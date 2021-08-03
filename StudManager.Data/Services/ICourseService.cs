using Microsoft.AspNetCore.Mvc;
using StudManager.Data.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StudManager.Data.Services
{
    public interface ICourseService
    {
        public IEnumerable<Course> GetAllCourse();
        public void AddCourse(Course model);

        public void UpdateCourse(Course model);

        public bool SaveAll();

        public Course GetCourse(int id);

    }
}
