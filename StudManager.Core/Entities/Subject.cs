using System.ComponentModel.DataAnnotations.Schema;

namespace StudManager.Core.Entities
{
    public class Subject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SubjectDescription { get; set; }
        public int Credit { get; set; }
        [ForeignKey("Course")]
        public int CourseId { get; set; }
        public virtual Course Course { get; set; }
    }
}
