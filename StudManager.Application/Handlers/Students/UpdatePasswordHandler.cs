using MediatR;
using StudManager.Application.Commands.Students;
using StudManager.Application.Responses.Common;
using StudManager.Core.Configuration;
using StudManager.Core.Entities;
using StudManager.Core.Models;
using System.Threading;
using System.Threading.Tasks;

namespace StudManager.Application.Handlers.Students
{
    public class UpdatePasswordHandler : IRequestHandler<UpdatePasswordCommand, Response>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdatePasswordHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Response> Handle(UpdatePasswordCommand request, CancellationToken cancellationToken)
        {
            var userExists = await _unitOfWork.Student.ExistUserByName(request.username);

            if (userExists == null)
                return new Response { Status = "Error", Message = "Student is not registered!" };

            ApplicationUser user = userExists;

            var result = await _unitOfWork.Student.ChangePassword(user, request.CurrentPasssword, request.NewPassword);

            if (!result)
            {
                return new Response { Status = "Error", Message = "Update password process is failed! Please check user details and try again." };
            }
            else
            {
                return new Response { Status = "Success", Message = "Password updated successfully!" };
            }


        }
    }
}
