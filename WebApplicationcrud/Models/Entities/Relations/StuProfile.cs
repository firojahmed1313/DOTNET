namespace WebApplicationcrud.Models.Entities.Relations
{
    public class StuProfile
    {
        public Guid Id { get; set; }
        public Guid StuId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }

        public Stu Student { get; set; } = null!;
    }
}
