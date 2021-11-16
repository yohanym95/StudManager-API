using AutoMapper;
using MediatR;
using StudManager.Application.Queries.Courses;
using StudManager.Application.Responses.Courses;
using StudManager.Core.Configuration;
using StudManager.Core.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace StudManager.Application.Handlers.Courses
{
    public class GetCourseHandler : IRequestHandler<GetCourseQuery, CourseResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetCourseHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<CourseResponse> Handle(GetCourseQuery request, CancellationToken cancellationToken)
        {

            var course = await _unitOfWork.Course.GetById(request.Id);
            var courseResponse = _mapper.Map<Course, CourseResponse>(course);
            return courseResponse;
        }
    }
}
