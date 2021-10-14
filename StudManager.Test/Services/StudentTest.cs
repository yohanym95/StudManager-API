using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using StudManager.Controllers.Students;
using StudManager.Data.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudManager.Test.Services
{
    public class StudentTest
    {
        public Mock<IUnitOfWork> mock = new Mock<IUnitOfWork>();
        public Mock<ILogger<StudentController>> mockIlogger = new Mock<ILogger<StudentController>>();
        public Mock<IMapper> mockIMapper = new Mock<IMapper>();


        public void checkStudentRegister()
        {

        }

    }
}
