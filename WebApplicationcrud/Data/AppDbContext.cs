
using Microsoft.EntityFrameworkCore;
using WebApplicationcrud.Models.Entities;

namespace WebApplicationcrud.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Emp> emps { get; set; }
        public DbSet<Blog> blogs { get; set; }
        public DbSet<AllValidation> AllValidations { get; set; }

        internal object FirstOrDefaultAsync(Func<object, bool> value)
        {
            throw new NotImplementedException();
        }
    }
}
