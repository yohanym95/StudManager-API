using MediatR;
using StudManager.Application.Responses.Subjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudManager.Application.Queries.Subjects
{
    public class GetSubjectQuery : IRequest<SubjectResponse>
    {
        public int Id { get; set; }
    }
}
