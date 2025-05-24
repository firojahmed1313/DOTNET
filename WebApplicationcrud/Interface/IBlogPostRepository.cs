using WebApplicationcrud.Models.Entities;

namespace WebApplicationcrud.Interface
{
    public interface IBlogPostRepository
    {
        Task<IEnumerable<Blog>> GetAllAsync();
        Task<Blog?> GetByIdAsync(Guid id);
        Task AddAsync(Blog blogPost);
        //Task UpdateAsync(Blog blogPost);
        Task DeleteAsync(Blog blogPost);
        Task<bool> SaveChangesAsync();
    }
}
