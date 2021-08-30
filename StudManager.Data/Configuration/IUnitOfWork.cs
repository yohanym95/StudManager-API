using StudManager.Data.Services;
using System.Threading.Tasks;

namespace StudManager.Data.Configuration
{
    public interface IUnitOfWork
    {
        ICourseService Courses { get; }
        Task CompleteAsync();
    }
}
