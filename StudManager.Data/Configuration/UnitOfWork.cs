using Microsoft.Extensions.Logging;
using StudManager.Data.Context;
using StudManager.Data.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StudManager.Data.Configuration
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DBContext _context;
        private readonly ILogger _logger;


        public ICourseService Courses { get; private set; }
        public IFeesServices Fees { get; private set; }
        public IStudentServices Student { get; private set; }

        public UnitOfWork(DBContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger("logs");

            Courses = new CourseServices(context, _logger);
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
