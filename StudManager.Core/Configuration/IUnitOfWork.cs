using StudManager.Core.Repositories;
using System.Threading.Tasks;

namespace StudManager.Core.Configuration
{
    public interface IUnitOfWork
    {
        ICourseServices Course { get; }
        IFeesServices Fees { get; }
        IStudentServices Student { get; }
        ISubjectServices Subject { get; }
        Task CompleteAsync();
    }
}
