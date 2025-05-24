using WebApplicationcrud.Models.Entities.Relations;

namespace WebApplicationcrud.Interface.Relations
{
    public interface IStuProfileRepository
    {
        Task<StuProfile?> GetByStuIdAsync(Guid stuId);
        Task<StuProfile> CreateAsync(StuProfile profile);
        Task UpdateAsync(StuProfile profile);
        Task DeleteAsync(Guid id);
        Task<bool> SaveChangesAsync();
    }

}
