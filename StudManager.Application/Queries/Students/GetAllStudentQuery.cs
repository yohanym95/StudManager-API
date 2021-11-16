using MediatR;
using StudManager.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudManager.Application.Queries.Students
{
    public class GetAllStudentQuery : IRequest<List<ApplicationUser>>
    {
    }
}
