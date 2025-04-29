namespace WebApplicationcrud.Models.Entities
{
    public class Blog
    {
        public Guid Id { get; set; }
        public required string Title { get; set; }
        public required string Content { get; set; }
        public required string Author { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
