using Microsoft.EntityFrameworkCore;
using WebApplicationcrud.Data;
using WebApplicationcrud.Interface;
using WebApplicationcrud.Models.Entities;

namespace WebApplicationcrud.Repository
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly AppDbContext _context;

        public BlogPostRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Blog>> GetAllAsync()
        {
            return await _context.blogs.ToListAsync();
        }

        public async Task<Blog?> GetByIdAsync(int id)
        {
            return await _context.blogs.FindAsync(id);
        }

        public async Task AddAsync(Blog blogPost)
        {
            await _context.blogs.AddAsync(blogPost);
        }

        public async Task UpdateAsync(Blog blogPost)
        {
             _context.blogs.Update(blogPost);
        }

        public async Task DeleteAsync(Blog blogPost)
        {
             _context.blogs.Remove(blogPost);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        

        
    }
}
