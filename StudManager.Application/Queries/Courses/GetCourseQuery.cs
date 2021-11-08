using MediatR;
using StudManager.Application.Responses.Courses;
using StudManager.Core.Entities;

namespace StudManager.Application.Queries.Courses
{
    public class GetCourseQuery : IRequest<CourseResponse>
    {
        public int Id { get; set; }
    }
}
