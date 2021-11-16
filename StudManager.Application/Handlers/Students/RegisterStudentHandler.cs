using MediatR;
using StudManager.Application.Commands.Students;
using StudManager.Application.Responses.Common;
using StudManager.Core.Configuration;
using StudManager.Core.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace StudManager.Application.Handlers.Students
{
    public class RegisterStudentHandler : IRequestHandler<RegisterCommand, Response>
    {
        private readonly IUnitOfWork _unitOfWork;
        public RegisterStudentHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Response> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
           
            var userExists = await _unitOfWork.Student.ExistUserByName(request.UserName);

            if (userExists != null)
                return new Response { Status = "Error", Message = "Student already exists!" };

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

            var result = await _unitOfWork.Student.CreateStudent(user, request.Password);

            if (result)
            {
                var response = new Response
                {
                    Status = "Sucess",
                    Message = "Student Created Sucessfully"
                };

                return response;
            }
            else
            {
                var response = new Response
                {
                    Status = "Error",
                    Message = "Student creation failed! Please check user details and try again."
                };

                return response;
            }

        }
    }
}
