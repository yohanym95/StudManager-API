using AutoMapper;
using MediatR;
using StudManager.Application.Queries.Students;
using StudManager.Application.Responses.Students;
using StudManager.Core.Configuration;
using StudManager.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StudManager.Application.Handlers.Students
{
    public class GetStudentHandler : IRequestHandler<GetStudentQuery,StudentResponse>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetStudentHandler(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<StudentResponse> Handle(GetStudentQuery request, CancellationToken cancellationToken)
        {
            var userExists = await _unitOfWork.Student.GetStudent(request.Id);

            if(userExists != null)
            {
                var student = _mapper.Map<ApplicationUser, StudentResponse>(userExists);
                return student;
            }
            else
            {
                return null;
            }
            

        }
    }
}
