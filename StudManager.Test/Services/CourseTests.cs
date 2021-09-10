using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using StudManager.Controllers.Courses;
using StudManager.Data.Configuration;
using StudManager.Data.Data.Entities;
using AutoMapper;
using Xunit;
using StudManager.Data.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System;

namespace StudManager.Test.Services
{

    public class CourseTests
    {
        public Mock<IUnitOfWork> mock = new Mock<IUnitOfWork>();


        [Fact]
        public void CheckAddCourse()
        {
            var mockIlogger = new Mock<ILogger<CourseController>>();
            var mockIMapper = new Mock<IMapper>();

            var moqCourse = new CourseModel
            {
                Id = 1,
                CourseName = "CIS",
                CourseNo = "APS_03",
                Qualifications = "MATHS"
            };

            var moqCoure = new Course
            {
                Id = 1,
                CourseName = "CIS",
                CourseNo = "APS_03",
                Qualifications = "MATHS"
            };            

            mockIMapper.Setup(s => s.Map<CourseModel, Course>(It.IsAny<CourseModel>())).Returns(moqCoure);

            mock.Setup(s => s.Courses.Add(It.IsAny<Course>())).ReturnsAsync(true);

            CourseController courseController = new CourseController(mockIlogger.Object, mock.Object, mockIMapper.Object);

            var result = courseController.Post(moqCourse);

            var okResult = result.Result as OkObjectResult;

            var resultItem = Assert.IsType<Course>(okResult.Value);

            Assert.True(moqCoure.Equals(resultItem));
        }

        [Fact]
        public void CheckGetCourse()
        {
            var mockIlogger = new Mock<ILogger<CourseController>>();
            var mockIMapper = new Mock<IMapper>();

            List<Course> courseList = new List<Course>();

            courseList.Add(new Course {
                Id = 1,
                CourseName = "CIS",
                CourseNo = "APS_03",
                Qualifications = "MATHS"
            });
            courseList.Add(new Course
            {
                Id = 2,
                CourseName = "APS",
                CourseNo = "APS_04",
                Qualifications = "BIO"
            });


            mock.Setup(s => s.Courses.All()).ReturnsAsync(courseList);

            CourseController courseController = new CourseController(mockIlogger.Object, mock.Object, mockIMapper.Object);

            var result = courseController.Get();

            var okResult = result.Result as OkObjectResult;

            var resultItem = Assert.IsType<List<Course>>(okResult.Value);

            Assert.Equal(2, resultItem.Count);
        }


        [Fact]
        public void CheckGetCourseById()
        {
            var mockIlogger = new Mock<ILogger<CourseController>>();
            var mockIMapper = new Mock<IMapper>();


            var moqCoure = new Course
            {
                Id = 1,
                CourseName = "CIS",
                CourseNo = "APS_03",
                Qualifications = "MATHS"
            };




            mock.Setup(s => s.Courses.GetById(It.IsAny<int>())).ReturnsAsync(moqCoure);

            CourseController courseController = new CourseController(mockIlogger.Object, mock.Object, mockIMapper.Object);

            var result = courseController.GetCourse(5);

            var okResult = result.Result as OkObjectResult;

            var resultItem = Assert.IsType<Course>(okResult.Value);

            Assert.Equal(moqCoure, resultItem);
        }


        [Fact]
        public void CheckCourseUpdate()
        {
            var mockIlogger = new Mock<ILogger<CourseController>>();
            var mockIMapper = new Mock<IMapper>();

            var moqCoure = new Course
            {
                Id = 1,
                CourseName = "CIS",
                CourseNo = "APS_03",
                Qualifications = "MATHS"
            };

            var moqCourse = new CourseModel
            {
                Id = 1,
                CourseName = "CIS",
                CourseNo = "APS_03",
                Qualifications = "MATHS"
            };

            mockIMapper.Setup(s => s.Map<CourseModel, Course>(It.IsAny<CourseModel>())).Returns(moqCoure);

            mock.Setup(s => s.Courses.Upsert(It.IsAny<Course>())).ReturnsAsync(true);

            CourseController courseController = new CourseController(mockIlogger.Object, mock.Object, mockIMapper.Object);

            var result = courseController.Update(moqCourse);

            var okResult = result.Result as OkObjectResult;

            if (okResult.StatusCode.Value == 200)
            {
                var resultItem = Assert.IsType<Course>(okResult.Value);

                Assert.True(moqCoure.Equals(resultItem));
            }
            
        }

        [Fact]
        public void CheckCourseDelete()
        {
            var mockIlogger = new Mock<ILogger<CourseController>>();
            var mockIMapper = new Mock<IMapper>();
            var moqCoure = new Course
            {
                Id = 1,
                CourseName = "CIS",
                CourseNo = "APS_03",
                Qualifications = "MATHS"
            };
            mock.Setup(s => s.Courses.GetById(It.IsAny<int>())).ReturnsAsync(moqCoure);

            mock.Setup(s => s.Courses.Delete(It.IsAny<int>())).ReturnsAsync(true);

            CourseController courseController = new CourseController(mockIlogger.Object, mock.Object, mockIMapper.Object);

            var result = courseController.DeleteItem(1);

            var okResult = result.Result as OkObjectResult;

            Assert.True(result.IsCompletedSuccessfully);

        }
    }
}
