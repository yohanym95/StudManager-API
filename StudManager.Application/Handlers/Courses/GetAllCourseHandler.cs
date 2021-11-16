using MediatR;
using StudManager.Application.Queries.Courses;
using StudManager.Core.Configuration;
using StudManager.Core.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace StudManager.Application.Handlers.Courses
{
    public class GetAllCourseHandler : IRequestHandler<GetAllCourseQuery, IEnumerable<Course>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetAllCourseHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public  async Task<IEnumerable<Course>> Handle(GetAllCourseQuery request, CancellationToken cancellationToken)
        {
            var courses = await _unitOfWork.Course.All();
            return courses;
        }
    }
}
