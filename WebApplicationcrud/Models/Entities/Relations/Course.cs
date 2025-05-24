namespace WebApplicationcrud.Models.Entities.Relations
{
    public class Course
    {
        public Guid CourseId { get; set; }
        public string CourseName { get; set; } = string.Empty;
        public string CourseType { get; set; } = string.Empty;

        public ICollection<Stu> Students { get; set; } = new List<Stu>();
    }
}
