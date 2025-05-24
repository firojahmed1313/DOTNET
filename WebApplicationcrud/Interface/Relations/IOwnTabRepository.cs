using WebApplicationcrud.Models.Entities.Relations;

namespace WebApplicationcrud.Interface.Relations
{
    public interface IOwnTabRepository
    {
        Task<IEnumerable<OwnTab>> GetByStuIdAsync(Guid stuId);
        Task<OwnTab> CreateAsync(OwnTab tab);
        Task DeleteAsync(Guid id);
        Task<bool> SaveChangesAsync();
    }

}
