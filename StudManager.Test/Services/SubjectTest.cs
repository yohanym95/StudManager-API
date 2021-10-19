using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using StudManager.Controllers.Subjects;
using StudManager.Data.Configuration;
using StudManager.Data.Data.Entities;
using StudManager.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace StudManager.Test.Services
{
    public class SubjectTest
    {
        public Mock<IUnitOfWork> mock = new Mock<IUnitOfWork>();
        public Mock<ILogger<SubjectController>> mockIlogger = new Mock<ILogger<SubjectController>>();
        public Mock<IMapper> mockIMapper = new Mock<IMapper>();

        [Fact]
        public void checkAddSubject()
        {
            var subjectModel = new SubjectModel
            {
                Id = 1,
                Name = "Business Alignment",
                SubjectDescription = "Higher level",
                Credit = 4,
                CourseId = 1
            };

            var subject = new Subject
            {
                Id = 1,
                Name = "Business Alignment",
                SubjectDescription = "Higher level",
                Credit = 4,
                CourseId = 1
            };

            mockIMapper.Setup(s => s.Map<SubjectModel, Subject>(It.IsAny<SubjectModel>())).Returns(subject);

            mock.Setup(s => s.Subject.Add(It.IsAny<Subject>())).ReturnsAsync(true);

            SubjectController subjectController = new SubjectController(mockIlogger.Object, mock.Object, mockIMapper.Object);

            var result = subjectController.Post(subjectModel);

            var okResult = result.Result as OkObjectResult;

            var resultItem = Assert.IsType<Subject>(okResult.Value);

            Assert.True(subject.Equals(resultItem));

        }

        [Fact]
        public void checkGetSubject()
        {
            List<Subject> subjects = new List<Subject>();

            subjects.Add(
                new Subject
                {
                    Id = 1,
                    Name = "Business Alignment",
                    SubjectDescription = "Higher level",
                    Credit = 4,
                    CourseId = 1
                }) ;

            subjects.Add(
                new Subject
                {
                    Id = 2,
                    Name = "Business Alignment",
                    SubjectDescription = "Higher level",
                    Credit = 4,
                    CourseId = 1
                });

            mock.Setup(s => s.Subject.All()).ReturnsAsync(subjects);

            SubjectController subjectController = new SubjectController(mockIlogger.Object, mock.Object, mockIMapper.Object);

            var result = subjectController.Get();

            var okResult = result.Result as OkObjectResult;

            var resultItem = Assert.IsType<List<Subject>>(okResult.Value);

            Assert.Equal(2, resultItem.Count);
        }

        [Fact]
        public void CheckGetFeesById()
        {
            var moqSubject = new Subject
            {
                Id = 2,
                Name = "Business Alignment",
                SubjectDescription = "Higher level",
                Credit = 4,
                CourseId = 1
            };

            mock.Setup(s => s.Subject.GetById(It.IsAny<int>())).ReturnsAsync(moqSubject);


            SubjectController subjectController = new SubjectController(mockIlogger.Object, mock.Object, mockIMapper.Object);

            var result = subjectController.Get(2);

            var okResult = result.Result as OkObjectResult;

            var resultItem = Assert.IsType<Subject>(okResult.Value);

            var obj1 = JsonConvert.SerializeObject(moqSubject);
            var obj2 = JsonConvert.SerializeObject(resultItem);

            Assert.Equal(obj1, obj2);
        }


        [Fact]
        public void CheckSubjectUpdate()
        {


            var moqSubject = new Subject
            {
                Id = 2,
                Name = "Business Alignment",
                SubjectDescription = "Higher level",
                Credit = 4,
                CourseId = 1
            };

            var subjectModel = new SubjectModel
            {
                Id = 1,
                Name = "Business Alignment-1",
                SubjectDescription = "Higher level",
                Credit = 4,
                CourseId = 1
            };

            mockIMapper.Setup(s => s.Map<SubjectModel, Subject>(It.IsAny<SubjectModel>())).Returns(moqSubject);

            mock.Setup(s => s.Subject.Upsert(It.IsAny<Subject>())).ReturnsAsync(true);

            SubjectController subjectController = new SubjectController(mockIlogger.Object, mock.Object, mockIMapper.Object);

            var result = subjectController.Update(subjectModel);

            var okResult = result.Result as OkObjectResult;

            if (okResult.StatusCode.Value == 200)
            {
                var resultItem = Assert.IsType<Subject>(okResult.Value);

                Assert.True(moqSubject.Equals(resultItem));
            }

        }


    }
}
