using System.ComponentModel.DataAnnotations;

namespace WebApplicationcrud.Models.Entities.Relations
{
    public class OwnTab
    {
        [Key]
        public Guid TabId { get; set; }
        public string CompanyName { get; set; } = string.Empty;
        public string TabType { get; set; } = string.Empty;

        public Guid StuId { get; set; }
        public Stu Student { get; set; } = null!;
    }
}
