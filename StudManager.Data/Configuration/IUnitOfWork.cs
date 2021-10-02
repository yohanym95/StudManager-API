using StudManager.Data.Services;
using System.Threading.Tasks;

namespace StudManager.Data.Configuration
{
    public interface IUnitOfWork
    {
        ICourseService Courses { get; }
        IFeesServices Fees { get; }
        IStudentServices Student { get; }

        IAdminService Admin { get; }
        Task CompleteAsync();
    }
}
