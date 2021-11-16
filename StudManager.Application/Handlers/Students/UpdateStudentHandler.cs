using MediatR;
using StudManager.Application.Commands.Students;
using StudManager.Application.Responses.Common;
using StudManager.Core.Configuration;
using StudManager.Core.Entities;
using StudManager.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StudManager.Application.Handlers.Students
{
    public class UpdateStudentHandler : IRequestHandler<UpdateCommand, Response>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateStudentHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Response> Handle(UpdateCommand request, CancellationToken cancellationToken)
        {
            var userExists = await _unitOfWork.Student.ExistUserById(request.Id);

            if (userExists == null)
                return new Response { Status = "Error", Message = "Student is not registered!" };

            ApplicationUser user = new ApplicationUser()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                BirthDate = request.BirthDate,
                PhoneNumber = request.PhoneNumber,
                Email = request.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = request.UserName,
                UserType = request.UserType,
                Student = new Student
                {
                    StudRegNo = request.StudentRegisterNo,
                    FullName = request.FirstName + " " + request.LastName,
                    CourseId = request.CourseId
                }

            };

            var result = _unitOfWork.Student.UpdateStudent(user);

            if (result > 0)
            {
                var response = new Response
                {
                    Status = "Sucess",
                    Message = "Student Updated Sucessfully"
                };

                return response;
            }
            else
            {
                var response = new Response
                {
                    Status = "Error",
                    Message = "Student update process failed! Please check user details and try again."
                };

                return response;
            }

        }
    }
}
