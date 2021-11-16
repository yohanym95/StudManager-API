using MediatR;
using StudManager.Application.Responses.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudManager.Application.Commands.Subjects
{
    public class CreateSubjectCommand : IRequest<Response>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SubjectDescription { get; set; }
        public int Credit { get; set; }
        public int CourseId { get; set; }
    }
}
