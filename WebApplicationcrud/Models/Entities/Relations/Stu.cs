namespace WebApplicationcrud.Models.Entities.Relations
{
    public class Stu
    {
        public Guid StuId { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public StuProfile? StuProfile { get; set; }

        public ICollection<Course> Courses { get; set; } = new List<Course>();
        public ICollection<OwnTab> Tabs { get; set; } = new List<OwnTab>();
    }
}
