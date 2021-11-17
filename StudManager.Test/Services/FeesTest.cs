using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using StudManager.Core.Entities;
using StudManager.Infrastructure.Data;
using StudManager.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace StudManager.Test.Services
{
    public class FeesTest
    {
        public Mock<ILogger<FeesServices>> mockILogger = new Mock<ILogger<FeesServices>>();


        [Fact]
        public async void CheckGetAllFees()
        {
            var builder = new DbContextOptionsBuilder();
            builder.UseInMemoryDatabase("GetAllFees");

            using (var context = new DBContext(builder.Options))
            {   
                context.Fees.Add(new Fees
                {
                    Id = 1,
                    FeesType = "Exam Fees",
                    AmountofFees = "Rs. 500.00",
                    RecieptNo = "01",
                    FeesDescription = "Fees for medical exam",
                    StuId = 1,
                    CourseId = 2                
                });

                context.SaveChanges();

            }
            using (var FeesContext = new DBContext(builder.Options))
            {

                FeesServices feesServices = new FeesServices(FeesContext, mockILogger.Object);
                var result = await feesServices.All();
                int count = result.ToList().Count();
                Assert.Equal(1, count);

            }

        }

        [Fact]
        public async void CheckGetFees()
        {
            int Id;
            var builder = new DbContextOptionsBuilder();
            builder.UseInMemoryDatabase("GetFees");

            var fees = new Fees
            {
                Id = 1,
                FeesType = "Exam Fees",
                AmountofFees = "Rs. 500.00",
                RecieptNo = "01",
                FeesDescription = "Fees for medical exam",
                StuId = 1,
                CourseId = 2
            };

            using (var context = new DBContext(builder.Options))
            {

                context.Fees.Add(fees);

                Id = fees.Id;
                context.SaveChanges();

            }
            using (var FeesContext = new DBContext(builder.Options))
            {

                FeesServices feesServices = new FeesServices(FeesContext, mockILogger.Object);
                var result = await feesServices.GetById(Id);
                var expected = JsonConvert.SerializeObject(fees);
                var actual = JsonConvert.SerializeObject(result);
                Assert.Equal(expected, actual);
            }
        }


        [Fact]
        public async void CheckAddFees()
        {
            var builder = new DbContextOptionsBuilder();
            builder.UseInMemoryDatabase("AddFees");

            var fees = new Fees
            {
                Id = 1,
                FeesType = "Exam Fees",
                AmountofFees = "Rs. 500.00",
                RecieptNo = "01",
                FeesDescription = "Fees for medical exam",
                StuId = 1,
                CourseId = 2
            };

            using (var Context = new DBContext(builder.Options))
            {
                FeesServices feesServices = new FeesServices(Context, mockILogger.Object);
                var result = await feesServices.Add(fees);
                Assert.True(result);
            }
        }

        [Fact]
        public async void CheckDeleteFees()
        {
            var builder = new DbContextOptionsBuilder();
            builder.UseInMemoryDatabase("DeleteFees");

            var fees = new Fees
            {
                Id = 1,
                FeesType = "Exam Fees",
                AmountofFees = "Rs. 500.00",
                RecieptNo = "01",
                FeesDescription = "Fees for medical exam",
                StuId = 1,
                CourseId = 2
            };

            using (var context = new DBContext(builder.Options))
            {

                context.Fees.Add(fees);
                context.SaveChanges();
            }


            using (var feesContext = new DBContext(builder.Options))
            {

                FeesServices feesServices = new FeesServices(feesContext, mockILogger.Object);
                var result = await feesServices.Delete(fees.Id);
                Assert.True(result);
            }
        }
    }
}
