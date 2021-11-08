using AutoMapper;
using MediatR;
using StudManager.Application.Commands.Courses;
using StudManager.Application.Responses.Courses;
using StudManager.Core.Configuration;
using StudManager.Core.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace StudManager.Application.Handlers.Courses
{
    public class CreateCourseHandler : IRequestHandler<CourseCommand, CourseResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateCourseHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<CourseResponse> Handle(CourseCommand request, CancellationToken cancellationToken)
        {
            var courseEntity = _mapper.Map<CourseCommand, Course>(request);

            if (courseEntity is null)
            {
                throw new ApplicationException("There is issue with mapper");
            }

            await _unitOfWork.Course.Add(courseEntity);
            var courseResponse = _mapper.Map<Course, CourseResponse>(courseEntity);
            return courseResponse;
        }
    }
}
