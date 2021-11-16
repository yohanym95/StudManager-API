//using AutoMapper;
//using Microsoft.Extensions.Logging;
//using Moq;
//using StudManager.Controllers.Courses;
//using Xunit;
//using System.Collections.Generic;
//using Microsoft.AspNetCore.Mvc;

//namespace StudManager.Test.Services
//{

//    public class CourseTests
//    {
//        public Mock<IUnitOfWork> mock = new Mock<IUnitOfWork>();
//        public Mock<ILogger<CourseController>> mockIlogger = new Mock<ILogger<CourseController>>();
//        public Mock<IMapper> mockIMapper = new Mock<IMapper>();


//        [Fact]
//        public void CheckAddCourse()
//        {
            

//            var moqCourse = new CourseModel
//            {
//                Id = 1,
//                CourseName = "CIS",
//                CourseNo = "APS_03",
//                Qualifications = "MATHS"
//            };

//            var moqCoure = new Course
//            {
//                Id = 1,
//                CourseName = "CIS",
//                CourseNo = "APS_03",
//                Qualifications = "MATHS"
//            };            

//            mockIMapper.Setup(s => s.Map<CourseModel, Course>(It.IsAny<CourseModel>())).Returns(moqCoure);

//            mock.Setup(s => s.Courses.Add(It.IsAny<Course>())).ReturnsAsync(true);

//            CourseController courseController = new CourseController(mockIlogger.Object, mock.Object, mockIMapper.Object);

//            var result = courseController.Post(moqCourse);

//            var okResult = result.Result as OkObjectResult;

//            var resultItem = Assert.IsType<Course>(okResult.Value);

//            Assert.True(moqCoure.Equals(resultItem));
//        }

//        [Fact]
//        public void CheckGetCourse()
//        {
          

//            List<Course> courseList = new List<Course>();

//            courseList.Add(new Course {
//                Id = 1,
//                CourseName = "CIS",
//                CourseNo = "APS_03",
//                Qualifications = "MATHS"
//            });
//            courseList.Add(new Course
//            {
//                Id = 2,
//                CourseName = "APS",
//                CourseNo = "APS_04",
//                Qualifications = "BIO"
//            });


//            mock.Setup(s => s.Courses.All()).ReturnsAsync(courseList);

//            CourseController courseController = new CourseController(mockIlogger.Object, mock.Object, mockIMapper.Object);

//            var result = courseController.Get();

//            var okResult = result.Result as OkObjectResult;

//            var resultItem = Assert.IsType<List<Course>>(okResult.Value);

//            Assert.Equal(2, resultItem.Count);
//        }


//        [Fact]
//        public void CheckGetCourseById()
//        {
            


//            var moqCoure = new Course
//            {
//                Id = 1,
//                CourseName = "CIS",
//                CourseNo = "APS_03",
//                Qualifications = "MATHS"
//            };

//            mock.Setup(s => s.Courses.GetById(It.IsAny<int>())).ReturnsAsync(moqCoure);

//            CourseController courseController = new CourseController(mockIlogger.Object, mock.Object, mockIMapper.Object);

//            var result = courseController.GetCourse(1);

//            var okResult = result.Result as OkObjectResult;

//            var resultItem = Assert.IsType<Course>(okResult.Value);

//            Assert.Equal(moqCoure, resultItem);
//        }


//        [Fact]
//        public void CheckCourseUpdate()
//        {
            

//            var moqCoure = new Course
//            {
//                Id = 1,
//                CourseName = "CIS",
//                CourseNo = "APS_03",
//                Qualifications = "MATHS"
//            };

//            var moqCourse = new CourseModel
//            {
//                Id = 1,
//                CourseName = "CIS",
//                CourseNo = "APS_03",
//                Qualifications = "MATHS"
//            };

//            mockIMapper.Setup(s => s.Map<CourseModel, Course>(It.IsAny<CourseModel>())).Returns(moqCoure);

//            mock.Setup(s => s.Courses.Upsert(It.IsAny<Course>())).ReturnsAsync(true);

//            CourseController courseController = new CourseController(mockIlogger.Object, mock.Object, mockIMapper.Object);

//            var result = courseController.Update(moqCourse);

//            var okResult = result.Result as OkObjectResult;

//            if (okResult.StatusCode.Value == 200)
//            {
//                var resultItem = Assert.IsType<Course>(okResult.Value);

//                Assert.True(moqCoure.Equals(resultItem));
//            }
            
//        }

//        [Fact]
//        public void CheckCourseDelete()
//        {
            
//            var moqCoure = new Course
//            {
//                Id = 1,
//                CourseName = "CIS",
//                CourseNo = "APS_03",
//                Qualifications = "MATHS"
//            };
//            mock.Setup(s => s.Courses.GetById(It.IsAny<int>())).ReturnsAsync(moqCoure);

//            mock.Setup(s => s.Courses.Delete(It.IsAny<int>())).ReturnsAsync(true);

//            CourseController courseController = new CourseController(mockIlogger.Object, mock.Object, mockIMapper.Object);

//            var result = courseController.DeleteItem(1);

//            var okResult = result.Result as OkObjectResult;

//            Assert.True(result.IsCompletedSuccessfully);

//        }
//    }
//}
