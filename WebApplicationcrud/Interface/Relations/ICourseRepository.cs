using WebApplicationcrud.Models.Entities.Relations;

namespace WebApplicationcrud.Interface.Relations
{
    public interface ICourseRepository
    {
        Task<IEnumerable<Course>> GetAllAsync();
        Task<Course?> GetByIdAsync(Guid id);
        Task<Course> CreateAsync(Course course);
        Task DeleteAsync(Guid id);
        Task<bool> SaveChangesAsync();
    }
}
