using WebApplicationcrud.Models.Entities.Relations;

namespace WebApplicationcrud.Interface.Relations
{
    public interface IStuRepository
    {
        Task<IEnumerable<Stu>> GetAllAsync();
        Task<Stu?> GetByIdAsync(Guid id);
        Task<Stu> CreateAsync(Stu stu);
        Task UpdateAsync(Stu stu);
        Task DeleteAsync(Guid id);
        Task<bool> SaveChangesAsync();
    }

}
