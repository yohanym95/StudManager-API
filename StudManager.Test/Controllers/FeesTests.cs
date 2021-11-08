//using AutoMapper;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Logging;
//using Moq;
//using Newtonsoft.Json;
//using StudManager.Controllers.Fee;
//using StudManager.Data.Configuration;
//using StudManager.Data.Data.Entities;
//using StudManager.Data.Models;
//using System;
//using System.Collections.Generic;
//using Xunit;

//namespace StudManager.Test.Services
//{
//    public class FeesTests
//    {
//        public Mock<IUnitOfWork> mock = new Mock<IUnitOfWork>();
//        public Mock<ILogger<FeesController>> mockIlogger = new Mock<ILogger<FeesController>>();
//        public Mock<IMapper> mockIMapper = new Mock<IMapper>();

//        [Fact]
//        public void checkAddFees()
//        {
//            var feesModel = new FeesModel
//            {
//                Id = 1,
//                FeesType = "Exam Fees",
//                AmountofFees = "Rs.500.00",
//                RecieptNo = "F1123",
//                FeesDescription = "description ",
//                StuId = 123
//            };

//            var fees = new Fees
//            {
//                Id = 1,
//                FeesType = "Exam Fees",
//                AmountofFees = "Rs.500.00",
//                RecieptNo = "F1123",
//                FeesDescription = "description ",
//                StuId = 123
//            };

//            mockIMapper.Setup(s => s.Map<FeesModel, Fees>(It.IsAny<FeesModel>())).Returns(fees);

//            mock.Setup(s => s.Fees.Add(It.IsAny<Fees>())).ReturnsAsync(true);

//            FeesController feesController = new FeesController(mockIlogger.Object, mock.Object, mockIMapper.Object);

//            var result = feesController.Post(feesModel);

//            var okResult = result.Result as OkObjectResult;

//            var resultItem = Assert.IsType<Fees>(okResult.Value);

//            Assert.True(fees.Equals(resultItem));

//        }


//        [Fact]
//        public void CheckGetFees()
//        {
//            List<Fees> feesList = new List<Fees>();

//            feesList.Add(
//                new Fees
//                {
//                    Id = 1,
//                    FeesType = "Exam Fees",
//                    AmountofFees = "Rs.500.00",
//                    RecieptNo = "F1123",
//                    FeesDescription = "description ",
//                    StuId = 123
//                });

//            feesList.Add(
//                new Fees
//                {
//                    Id = 2,
//                    FeesType = "Exam Fees",
//                    AmountofFees = "Rs.500.00",
//                    RecieptNo = "F1124",
//                    FeesDescription = "description ",
//                    StuId = 124
//                });

//            mock.Setup(s => s.Fees.All()).ReturnsAsync(feesList);

//            FeesController feesController = new FeesController(mockIlogger.Object, mock.Object, mockIMapper.Object);

//            var result = feesController.Get();

//            var okResult = result.Result as OkObjectResult;

//            var resultItem = Assert.IsType<List<Fees>>(okResult.Value);

//            Assert.Equal(2, resultItem.Count);

//        }

//        [Fact]
//        public void CheckGetFeesById()
//        {
//            var moqFees = new Fees
//            {
//                Id = 2,
//                FeesType = "Exam Fees",
//                AmountofFees = "Rs.500.00",
//                RecieptNo = "F1124",
//                FeesDescription = "description ",
//                StuId = 124
//            };

//            var feesViewModel = new FeesViewModel
//            {
//                Id = 2,
//                FeesType = "Exam Fees",
//                AmountofFees = "Rs.500.00",
//                RecieptNo = "F1124",
//                FeesDescription = "description ",
//                StuId = 124,
//                studName = "A.Y Malshika",
//                studRegNo = "15APC2366"
//            };

//            var student = new Student
//            {
//               Id =  124,
//               StudRegNo = "15APC2366",
//               FullName = "A.Y Malshika"
//            };


//            mock.Setup(s => s.Fees.GetById(It.IsAny<int>())).ReturnsAsync(moqFees);

//            mock.Setup(s => s.Student.GetStudent(It.IsAny<int>())).ReturnsAsync(student);

//            FeesController feesController = new FeesController(mockIlogger.Object, mock.Object, mockIMapper.Object);

//            var result = feesController.GetFees(2);

//            var okResult = result.Result as OkObjectResult;

//            var resultItem = Assert.IsType<FeesViewModel>(okResult.Value);

//            var obj1 = JsonConvert.SerializeObject(feesViewModel);
//            var obj2 = JsonConvert.SerializeObject(resultItem);

//            Assert.Equal(obj1,obj2);
//        }
//    }
//}
