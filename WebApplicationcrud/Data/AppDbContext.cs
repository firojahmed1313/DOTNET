
using Microsoft.EntityFrameworkCore;
using WebApplicationcrud.Models.Entities;
using WebApplicationcrud.Models.Entities.Relations;

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

        public DbSet<Stu> Students { get; set; }
        public DbSet<StuProfile> StuProfiles { get; set; }
        public DbSet<OwnTab> Tabs { get; set; }
        public DbSet<Course> Courses { get; set; }

        internal object FirstOrDefaultAsync(Func<object, bool> value)
        {
            throw new NotImplementedException();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Stu>()
                .HasOne(s => s.StuProfile)
                .WithOne(p => p.Student)
                .HasForeignKey<StuProfile>(p => p.StuId);

            modelBuilder.Entity<Stu>()
                .HasMany(s => s.Tabs)
                .WithOne(t => t.Student);

            modelBuilder.Entity<Course>()
                .HasMany(c => c.Students)
                .WithMany(s => s.Courses);
        }
    }
}
