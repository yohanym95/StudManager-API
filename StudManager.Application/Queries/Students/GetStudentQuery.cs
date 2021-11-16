using MediatR;
using StudManager.Application.Responses.Students;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudManager.Application.Queries.Students
{
    public class GetStudentQuery : IRequest<StudentResponse>
    {

        public string Id { get; set; }
    }
}
