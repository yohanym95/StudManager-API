using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using StudManager.Core.Entities;
using StudManager.Infrastructure.Data;
using StudManager.Infrastructure.Repositories;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace StudManager.Test.Services
{
    public class CourseTest
    {
        public Mock<ILogger<CourseServices>> mockILogger = new Mock<ILogger<CourseServices>>();

        [Fact]
        public async void CheckGetAllCourse()
        {
            var builder = new DbContextOptionsBuilder();
            builder.UseInMemoryDatabase("GetAllCourses");

            using (var context = new DBContext(builder.Options))
            {
                
                context.Courses.Add(new Course
                {
                    Id = 1,
                    CourseName = "CIS",
                    CourseNo = "APS_03",
                    Qualifications = "MATHS"
                });

                context.SaveChanges();

            }
            using (var CourseContext = new DBContext(builder.Options))
            {

                CourseServices courseServices = new CourseServices(CourseContext, mockILogger.Object);
                var result = await courseServices.All();
                int count = result.ToList().Count();
                Assert.Equal(1, count);

            }
        }

        [Fact]
        public async void CheckGetCourse()
        {
            int Id;
            var builder = new DbContextOptionsBuilder();
            builder.UseInMemoryDatabase("GetCourses");

            var course = new Course
            {
                Id = 1,
                CourseName = "CIS",
                CourseNo = "APS_03",
                Qualifications = "MATHS"
            };

            using (var context = new DBContext(builder.Options))
            {

                context.Courses.Add(course);

                Id = course.Id;
                context.SaveChanges();

            }
            using (var CourseContext = new DBContext(builder.Options))
            {

                CourseServices courseServices = new CourseServices(CourseContext, mockILogger.Object);
                var result = await courseServices.GetById(Id);
                var expected = JsonConvert.SerializeObject(course);
                var actual = JsonConvert.SerializeObject(result);
                Assert.Equal(expected, actual);
            }
        }


        [Fact]
        public async void CheckUpdateCourse()
        {
            var builder = new DbContextOptionsBuilder();
            builder.UseInMemoryDatabase("UpdateCourses");

            var course = new Course
            {
                Id = 1,
                CourseName = "CIS",
                CourseNo = "APS_03",
                Qualifications = "MATHS"
            };

            using (var context = new DBContext(builder.Options))
            {

                context.Courses.Add(course);
                context.SaveChanges();
            }


            using (var CourseContext = new DBContext(builder.Options))
            {

                CourseServices courseServices = new CourseServices(CourseContext, mockILogger.Object);
                var result = await courseServices.Upsert(course);
                Assert.True(result);
            }
        }

        [Fact]
        public async void CheckAddCourse()
        {
            var builder = new DbContextOptionsBuilder();
            builder.UseInMemoryDatabase("AddCourses");

            var course = new Course
            {
                Id = 1,
                CourseName = "CIS",
                CourseNo = "APS_03",
                Qualifications = "MATHS"
            };


            using (var CourseContext = new DBContext(builder.Options))
            {

                CourseServices courseServices = new CourseServices(CourseContext, mockILogger.Object);
                var result = await courseServices.Add(course);
                Assert.True(result);
            }
        }


        [Fact]
        public async void CheckDeleteCourse()
        {
            var builder = new DbContextOptionsBuilder();
            builder.UseInMemoryDatabase("DeleteCourses");

            var course = new Course
            {
                Id = 1,
                CourseName = "CIS",
                CourseNo = "APS_03",
                Qualifications = "MATHS"
            };

            using (var context = new DBContext(builder.Options))
            {

                context.Courses.Add(course);
                context.SaveChanges();
            }


            using (var CourseContext = new DBContext(builder.Options))
            {

                CourseServices courseServices = new CourseServices(CourseContext, mockILogger.Object);
                var result = await courseServices.Delete(course.Id);
                Assert.True(result);
            }
        }
    }
}
