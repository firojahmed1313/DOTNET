namespace WebApplicationcrud.Models.Entities
{
    public class Emp
    {
        public  Guid Id { get; set; }
        public required string Username { get; set; }
        public required string Email { get; set; }

        public required  string Password { get; set; }

       



    }
}
