using MediatR;
using StudManager.Application.Commands.Courses;
using StudManager.Application.Responses.Courses;
using StudManager.Core.Configuration;
using System.Threading;
using System.Threading.Tasks;

namespace StudManager.Application.Handlers.Courses
{
    public class DeleteCourseHandler : IRequestHandler<DeleteCourseCommand, DeleteCourseResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCourseHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<DeleteCourseResponse> Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
        {
            var delteCourse = await _unitOfWork.Course.Delete(request.Id);
            var resposne = new DeleteCourseResponse
            {
                Message = "Successfully Deleted"
            };
            return resposne;
        }
    }
}
