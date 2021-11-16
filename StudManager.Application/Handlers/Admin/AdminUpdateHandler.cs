using MediatR;
using StudManager.Application.Commands.Admin;
using StudManager.Application.Responses.Common;
using StudManager.Core.Entities;
using StudManager.Core.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace StudManager.Application.Handlers.Admin
{

    public class AdminUpdateHandler : IRequestHandler<UpdateAdminCommand, Response>
    {
        private readonly IAdminServices _adminService;

        public AdminUpdateHandler(IAdminServices adminService)
        {
            _adminService = adminService;
        }

        public async Task<Response> Handle(UpdateAdminCommand request, CancellationToken cancellationToken)
        {
            var userExists = await _adminService.ExistUser(request.UserName);
            if (userExists == null)
                return new Response { Status = "Error", Message = "User  is not registered!" };

            ApplicationUser user = new ApplicationUser()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                BirthDate = request.BirthDate,
                PhoneNumber = request.PhoneNumber,
                Email = request.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = request.UserName,
                UserType = "Admin"

            };
            var result = await _adminService.UpdateManagementUser(user);

            if (!result)
            {
                return new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." };
            }
            else
            {
                return new Response { Status = "Success", Message = "User created successfully!" };
            }
        }
    }
}
