using MediatR;
using StudManager.Application.Responses.Courses;
using System.ComponentModel.DataAnnotations;

namespace StudManager.Application.Commands.Courses
{
    public class CourseCommand : IRequest<CourseResponse>
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Course Name Required")]
        public string CourseName { get; set; }
        public string Qualifications { get; set; }
        [Required(ErrorMessage = "Course Number Required")]
        public string CourseNo { get; set; }
    }
}
