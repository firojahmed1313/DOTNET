using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplicationcrud.Data;
using WebApplicationcrud.Models.Entities.Relations;

namespace WebApplicationcrud.Controllers.Relations
{
    [Route("api/[controller]")]
    [ApiController]
    public class StuProfileController : ControllerBase
    {
        private readonly AppDbContext _context;

        public StuProfileController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StuProfile>>> GetProfiles()
        {
            return await _context.StuProfiles.Include(p => p.Student).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StuProfile>> GetProfile(Guid id)
        {
            var profile = await _context.StuProfiles.Include(p => p.Student).FirstOrDefaultAsync(p => p.Id == id);
            if (profile == null) return NotFound();
            return profile;
        }

        [HttpPost]
        public async Task<ActionResult<StuProfile>> CreateProfile(StuProfile profile)
        {
            _context.StuProfiles.Add(profile);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetProfile), new { id = profile.Id }, profile);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProfile(Guid id, StuProfile profile)
        {
            if (id != profile.Id) return BadRequest();
            _context.Entry(profile).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProfile(Guid id)
        {
            var profile = await _context.StuProfiles.FindAsync(id);
            if (profile == null) return NotFound();
            _context.StuProfiles.Remove(profile);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }

}
