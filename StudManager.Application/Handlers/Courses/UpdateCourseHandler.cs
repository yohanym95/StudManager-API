using AutoMapper;
using MediatR;
using StudManager.Application.Commands.Courses;
using StudManager.Application.Responses.Courses;
using StudManager.Core.Configuration;
using StudManager.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StudManager.Application.Handlers.Courses
{
    public class UpdateCourseHandler : IRequestHandler<CourseCommand, CourseResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UpdateCourseHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CourseResponse> Handle(CourseCommand request, CancellationToken cancellationToken)
        {
            var courseEntity = _mapper.Map<CourseCommand, Course>(request);

            if(courseEntity is null)
            {
                throw new ApplicationException("Issue with mapper");
            }

            var newCourse = await _unitOfWork.Course.Upsert(courseEntity);
            

            if (newCourse)
            {
                var courseResponse = _mapper.Map<Course, CourseResponse>(courseEntity);
                return courseResponse;
            }
            else
            {
                throw new ApplicationException("There is issue with update service, Try again");
            }
        }
    }
}
