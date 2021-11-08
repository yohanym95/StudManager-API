using MediatR;
using StudManager.Application.Queries.Students;
using StudManager.Core.Configuration;
using StudManager.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StudManager.Application.Handlers.Students
{
    public class GetAllStudentHandler : IRequestHandler<GetAllStudentQuery, List<ApplicationUser>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllStudentHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<List<ApplicationUser>> Handle(GetAllStudentQuery request, CancellationToken cancellationToken)
        {
            var result =  _unitOfWork.Student.GetAllStudents();
            return result;
        }

    }
}
