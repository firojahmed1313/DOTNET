using System.Reflection.Metadata;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplicationcrud.Interface;
using WebApplicationcrud.Models;
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

        public async Task<bool> UpdateAsync(Guid id, BlogDto post)
        {
            //await _repo.UpdateAsync(post);
            var existingPost = await _repo.GetByIdAsync(id);
            if (existingPost == null) return false;

            //if (!string.IsNullOrWhiteSpace(post.Title))
            //    existingPost.Title = post.Title;

            //if (!string.IsNullOrWhiteSpace(post.Content))
            //    existingPost.Content = post.Content;

            var dtoType = typeof(BlogDto);
            var entityType = typeof(Blog);

            foreach (var prop in dtoType.GetProperties())
            {
                var newValue = prop.GetValue(post);
                if (newValue != null)
                {
                    var blogProp = entityType.GetProperty(prop.Name);
                    if (blogProp != null && blogProp.CanWrite)
                    {
                        blogProp.SetValue(existingPost, newValue);
                    }
                }
            }
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

