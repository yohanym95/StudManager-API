using MediatR;
using StudManager.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudManager.Application.Queries.Courses
{
    public class GetAllCourseQuery : IRequest<IEnumerable<Course>>
    {
    }
}
