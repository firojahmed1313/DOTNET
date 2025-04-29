using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplicationcrud.Data;
using WebApplicationcrud.Models;
using WebApplicationcrud.Models.Entities;

namespace WebApplicationcrud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public BlogPostsController(AppDbContext appDbContext, IMapper blogProfile)
        {
            this._context = appDbContext;
            this._mapper = blogProfile;


        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Blog>>> GetPosts()
        {
            return await _context.blogs.ToListAsync();
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Blog>> GetPost(Guid id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var username = User.FindFirstValue(ClaimTypes.Name);
            var email = User.FindFirstValue(ClaimTypes.Email);
            var post = await _context.blogs.FindAsync(id);
            if (post == null) return NotFound();
            return Ok( new { post, userId, username, email });
        }
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Blog>> CreatePost(Blog post)
        {
            post.CreatedAt = DateTime.UtcNow;
            _context.blogs.Add(post);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetPost), new { id = post.Id }, post);
        }
        //[Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePost(Guid id, BlogDto post)
        {
            if (id != post.Id) return BadRequest();
            var existingPost = await _context.blogs.FindAsync(id);
            if (existingPost == null) return NotFound();
            existingPost.Title = post.Title;
            existingPost.Content = post.Content;
            await _context.SaveChangesAsync();

            return Ok(existingPost);
        }


        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchPost(Guid id, [FromBody] JsonPatchDocument<BlogDto> patchDto)
        {
            if (patchDto == null) return BadRequest();

            var blog = await _context.blogs.FindAsync(id);
            if (blog == null) return NotFound();

            var blogDto = _mapper.Map<BlogDto>(blog); // Map entity to DTO

            patchDto.ApplyTo(blogDto,ModelState); // Apply patch to DTO

            if (!ModelState.IsValid) return BadRequest(ModelState);

            _mapper.Map(blogDto, blog); // Map updated DTO back to entity

            await _context.SaveChangesAsync();
            return Ok(blog);
        }

        [HttpPatch("simple/{id}")]
        public async Task<IActionResult> SimplePatchPost(Guid id, [FromBody] BlogDto dto)
        {
            var blog = await _context.blogs.FindAsync(id);
            if (blog == null) return NotFound();

            //if (!string.IsNullOrWhiteSpace(dto.Title))
            //    blog.Title = dto.Title;

            //if (!string.IsNullOrWhiteSpace(dto.Content))
            //    blog.Content = dto.Content;

            var dtoType = typeof(BlogDto);
            var entityType = typeof(Blog);

            foreach (var prop in dtoType.GetProperties())
            {
                var newValue = prop.GetValue(dto);
                if (newValue != null)
                {
                    var blogProp = entityType.GetProperty(prop.Name);
                    if (blogProp != null && blogProp.CanWrite)
                    {
                        blogProp.SetValue(blog, newValue);
                    }
                }
            }

            await _context.SaveChangesAsync();
            return Ok(blog);
        }


        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(Guid id)
        {
            var post = await _context.blogs.FindAsync(id);
            if (post == null) return NotFound();
            _context.blogs.Remove(post);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
