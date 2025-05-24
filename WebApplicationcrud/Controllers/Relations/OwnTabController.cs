using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplicationcrud.Data;
using WebApplicationcrud.Models.Entities.Relations;

namespace WebApplicationcrud.Controllers.Relations
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwnTabController : ControllerBase
    {
        private readonly AppDbContext _context;

        public OwnTabController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OwnTab>>> GetTabs()
        {
            return await _context.Tabs.Include(t => t.Student).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OwnTab>> GetTab(Guid id)
        {
            var tab = await _context.Tabs.Include(t => t.Student).FirstOrDefaultAsync(t => t.TabId == id);
            if (tab == null) return NotFound();
            return tab;
        }

        [HttpPost]
        public async Task<ActionResult<OwnTab>> CreateTab(OwnTab tab)
        {
            _context.Tabs.Add(tab);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetTab), new { id = tab.TabId }, tab);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTab(Guid id, OwnTab tab)
        {
            if (id != tab.TabId) return BadRequest();
            _context.Entry(tab).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTab(Guid id)
        {
            var tab = await _context.Tabs.FindAsync(id);
            if (tab == null) return NotFound();
            _context.Tabs.Remove(tab);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }

}
