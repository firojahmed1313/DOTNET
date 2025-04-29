using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplicationcrud.Models.Entities;
using WebApplicationcrud.Service;

namespace WebApplicationcrud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogNewController : ControllerBase
    {
        private readonly BlogPostService _service;

        public BlogNewController(BlogPostService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetPosts()
        {
            var posts = await _service.GetAllAsync();
            return Ok(posts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPost(Guid id)
        {
            var post = await _service.GetByIdAsync(id);
            if (post == null) return NotFound();
            return Ok(post);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost([FromBody] Blog post)
        {
            var result = await _service.CreateAsync(post);
            if (!result) return BadRequest();
            return CreatedAtAction(nameof(GetPost), new { id = post.Id }, post);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePost(Guid id, [FromBody] Blog post)
        {
            if (id != post.Id) return BadRequest();
            var result = await _service.UpdateAsync(post);
            return result ? NoContent() : BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(Guid id)
        {
            var result = await _service.DeleteAsync(id);
            return result ? NoContent() : NotFound();
        }
    }
}
