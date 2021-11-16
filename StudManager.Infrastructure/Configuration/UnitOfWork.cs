using Microsoft.Extensions.Logging;
using StudManager.Core.Configuration;
using StudManager.Core.Repositories;
using StudManager.Infrastructure.Data;
using StudManager.Infrastructure.Repositories;
using System.Threading.Tasks;

namespace StudManager.Infrastructure.Configuration
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DBContext _context;
        private readonly ILogger _logger;
        public ICourseServices Course { get; private set; }

        public IFeesServices Fees { get; private set; }

        public IStudentServices Student { get; private set; }

        public ISubjectServices Subject { get; private set; }


        public Task CompleteAsync()
        {
            throw new System.NotImplementedException();
        }

        public UnitOfWork(DBContext context, ILoggerFactory logger)
        {
            _context = context;
            _logger = logger.CreateLogger("logs");

            Course = new CourseServices(_context, _logger);

            
        }
    }
}
