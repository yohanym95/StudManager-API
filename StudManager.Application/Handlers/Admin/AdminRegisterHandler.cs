using MediatR;
using StudManager.Application.Commands.Admin;
using StudManager.Application.Responses.Common;
using StudManager.Core.Entities;
using StudManager.Core.Models;
using StudManager.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StudManager.Application.Handlers.Admin
{
    public class AdminRegisterHandler : IRequestHandler<AdminRegisterCommand, Response>
    {
        private readonly IAdminServices _adminService;

        public AdminRegisterHandler(IAdminServices adminService)
        {
            _adminService = adminService;
        }

        public async Task<Response> Handle(AdminRegisterCommand request, CancellationToken cancellationToken)
        {
            var userExists = await _adminService.ExistUser(request.UserName);
            if (userExists != null)
                return new Response { Status = "Error", Message = "User already exists!" };

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
            var result = await _adminService.CreateManagementUser(user, request.Password);

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
