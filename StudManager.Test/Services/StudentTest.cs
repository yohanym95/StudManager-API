using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using StudManager.Controllers.Students;
using StudManager.Data.Configuration;
using StudManager.Data.Data.Entities;
using StudManager.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace StudManager.Test.Services
{
    public class StudentTest
    {
        public Mock<IUnitOfWork> mock = new Mock<IUnitOfWork>();

        [Fact]
        public void checkStudentRegister()
        {

            ApplicationUser user = new ApplicationUser()
            {
                FirstName = "Yohan",
                LastName = "Malshika",
                BirthDate = "1995-09-30",
                PhoneNumber = "0701397674",
                Email = "malshikay@gmail.com",
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = "stud96",
                UserType = "Student",
                Student = new Student
                {
                    StudRegNo = "12345",
                    FullName = "Yohan Malshika",
                    CourseId = 1
                }
            };

            RegisterModel registerModel = new RegisterModel
            {
                FirstName = "Yohan",
                LastName = "Malshika",
                BirthDate = "1995-09-30",
                Password = "1234567",
                PhoneNumber = "0701397674",
                Email = "malshikay@gmail.com",
                UserName = "stud96",
                UserType = "Student",
                StudentRegisterNo = "12345",
                CourseId = 1
            };

            var response = new ResponseModel { Status = "Success", Message = "User created successfully!" };

            mock.Setup(s => s.Student.ExistUserByName(It.IsAny<string>())).ReturnsAsync((ApplicationUser)null);

            mock.Setup(s => s.Student.CreateStudent(user, registerModel.Password)).ReturnsAsync(true);

            StudentController studentController = new StudentController(mock.Object);

            var result = studentController.Register(registerModel);

            var okResult = result.Result as OkObjectResult;

            var resultItems = Assert.IsType<ResponseModel>(okResult.Value);

            var obj1 = JsonConvert.SerializeObject(response);
            var obj2 = JsonConvert.SerializeObject(resultItems);

            Assert.Equal(obj1, obj2);
        }

        [Fact]
        public void checkStudentUpdate()
        {

            ApplicationUser userExist = new ApplicationUser()
            {
                FirstName = "Yohan",
                LastName = "Malshika",
                BirthDate = "1995-09-30",
                PhoneNumber = "0701397674",
                Email = "malshikay@gmail.com",
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = "stud96",
                UserType = "Student",
                Student = new Student
                {
                    StudRegNo = "12345",
                    FullName = "Yohan Malshika"
                }
            };

            ApplicationUser user = new ApplicationUser()
            {
                FirstName = "Yohan",
                LastName = "Malshika",
                BirthDate = "1995-09-30",
                PhoneNumber = "0701397674",
                Email = "malshikay@gmail.com",
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = "stud96",
                UserType = "Student",
                Student = new Student
                {
                    StudRegNo = "12345",
                    FullName = "Yohan Malshika"
                }
            };

            RegisterModel registerModel = new RegisterModel
            {
                FirstName = "Yohan",
                LastName = "Malshika",
                BirthDate = "1995-09-30",
                PhoneNumber = "0701397674",
                Email = "malshikay@gmail.com",
                UserName = "stud96",
                UserType = "Student",
                StudentRegisterNo = "12345",
            };

            var response = new ResponseModel { Status = "Success", Message = "User updated successfully!" };

            mock.Setup(s => s.Student.ExistUserById(It.IsAny<string>())).ReturnsAsync(userExist);

            mock.Setup(s => s.Student.UpdateStudent(user)).Returns(3);

            StudentController studentController = new StudentController(mock.Object);

            var result = studentController.Update(registerModel,"1");

            var okResult = result.Result as OkObjectResult;

            var resultItems = Assert.IsType<ResponseModel>(okResult.Value);

            var obj1 = JsonConvert.SerializeObject(response);
            var obj2 = JsonConvert.SerializeObject(resultItems);

            Assert.Equal(obj1, obj2);
        }

        [Fact]
        public void checkStudentUpdatePassword()
        {

            ApplicationUser userExist = new ApplicationUser()
            {
                FirstName = "Yohan",
                LastName = "Malshika",
                BirthDate = "1995-09-30",
                PhoneNumber = "0701397674",
                Email = "malshikay@gmail.com",
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = "stud96",
                UserType = "Student",
                Student = new Student
                {
                    StudRegNo = "12345",
                    FullName = "Yohan Malshika"
                }
            };

           

            var passwordModel = new PasswordModel { username = "stud96", CurrentPasssword = "stud@!234", NewPassword = "stud@11234" };
            var response = new ResponseModel { Status = "Success", Message = "Password updated successfully!" };

            mock.Setup(s => s.Student.ExistUserByName(It.IsAny<string>())).ReturnsAsync(userExist);

            mock.Setup(s => s.Student.ChangePassword(It.IsAny<ApplicationUser>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(true);

            StudentController studentController = new StudentController(mock.Object);

            var result = studentController.UpdatePassword(passwordModel);

            var okResult = result.Result as OkObjectResult;

            var resultItems = Assert.IsType<ResponseModel>(okResult.Value);

            var obj1 = JsonConvert.SerializeObject(response);
            var obj2 = JsonConvert.SerializeObject(resultItems);

            Assert.Equal(obj1, obj2);
        }

        [Fact]
        public void checkStudentGet()
        {
            List<ApplicationUser> students = new List<ApplicationUser>();

            students.Add(new ApplicationUser()
            {
                FirstName = "Yohan",
                LastName = "Malshika",
                BirthDate = "1995-09-30",
                PhoneNumber = "0701397674",
                Email = "malshikay@gmail.com",
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = "stud96",
                UserType = "Student",
                Student = new Student
                {
                    StudRegNo = "12345",
                    FullName = "Yohan Malshika"
                }
            });
            students.Add(new ApplicationUser()
            {
                FirstName = "Yohan",
                LastName = "Malshika",
                BirthDate = "1995-09-30",
                PhoneNumber = "0701397674",
                Email = "malshikay96@gmail.com",
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = "stud97",
                UserType = "Student",
                Student = new Student
                {
                    StudRegNo = "12348",
                    FullName = "Yohan Malshika"
                }
            });


            mock.Setup(s => s.Student.GetAllStudents()).Returns(students);

            StudentController studentController = new StudentController(mock.Object);

            var result = studentController.Get();

            var okResult = result.Result as OkObjectResult;

            var resultItems = Assert.IsType<List<ApplicationUser>>(okResult.Value);


            Assert.Equal(students.Count, resultItems.Count);
        }

        [Fact]
        public void checkStudentGetStudent()
        {

            ApplicationUser student = new ApplicationUser()
            {
                FirstName = "Yohan",
                LastName = "Malshika",
                BirthDate = "1995-09-30",
                PhoneNumber = "0701397674",
                Email = "malshikay@gmail.com",
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = "stud96",
                UserType = "Student",
                Student = new Student
                {
                    StudRegNo = "12345",
                    FullName = "Yohan Malshika"
                }
            };


            mock.Setup(s => s.Student.GetStudent(It.IsAny<string>())).ReturnsAsync(student);

            StudentController studentController = new StudentController(mock.Object);

            var result = studentController.GetStudent("12345");
               

            var okResult = result.Result as OkObjectResult;

            var resultItems = Assert.IsType<ApplicationUser>(okResult.Value);


            Assert.Equal(student, resultItems);
        }


    }
}
