using MediatR;
using StudManager.Application.Responses.Courses;

namespace StudManager.Application.Commands.Courses
{
    public class DeleteCourseCommand  : IRequest<DeleteCourseResponse>
    {
        public int Id { get; set; }
    }
}
