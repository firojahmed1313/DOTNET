using System.ComponentModel.DataAnnotations;

namespace WebApplicationcrud.Models.Entities
{
    public class AllValidation
    {
        
        
            [Key]
            public Guid Id { get; set; }

            [Required]
            [StringLength(30, MinimumLength = 3)]
            public required string Username { get; set; }

            [Required]
            [EmailAddress]
            public required string Email { get; set; }


            [MinLength(6)]
            [MaxLength(16)]
            [DataType(DataType.Password)]
            [Required]
            public required string Password { get; set; }

            [Compare("Password")]
            [DataType(DataType.Password)]
            public required string ConfirmPassword { get; set; }

            [Phone]
            public string? PhoneNumber { get; set; }

            [Url]
            public string? Website { get; set; }

            [Range(18, 99)]
            public int Age { get; set; }

            [DataType(DataType.Date)]
            public DateTime BirthDate { get; set; }
        

    }
}
