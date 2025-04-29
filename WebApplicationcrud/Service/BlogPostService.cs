using WebApplicationcrud.Interface;
using WebApplicationcrud.Models.Entities;

namespace WebApplicationcrud.Service
{
    public class BlogPostService
    {
        private readonly IBlogPostRepository _repo;

        public BlogPostService(IBlogPostRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<Blog>> GetAllAsync()
        {
            return await _repo.GetAllAsync();
        }

        public async Task<Blog?> GetByIdAsync(Guid id)
        {
            return await _repo.GetByIdAsync(id);
        }

        public async Task<bool> CreateAsync(Blog post)
        {
            post.CreatedAt = DateTime.UtcNow;
            await _repo.AddAsync(post);
            return await _repo.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(Blog post)
        {
            await _repo.UpdateAsync(post);
            return await _repo.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var post = await _repo.GetByIdAsync(id);
            if (post == null) return false;
            await _repo.DeleteAsync(post);
            return await _repo.SaveChangesAsync();
        }
    }
}

