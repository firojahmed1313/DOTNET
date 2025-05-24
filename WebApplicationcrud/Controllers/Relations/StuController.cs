using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplicationcrud.Data;
using WebApplicationcrud.Models.Entities.Relations;

namespace WebApplicationcrud.Controllers.Relations
{
    [Route("api/[controller]")]
    [ApiController]
    public class StuController : ControllerBase
    {
        private readonly AppDbContext _context;

        public StuController(AppDbContext context)
        {
            _context = context;
        }

        // GET all students with profile and tabs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Stu>>> GetStudents()
        {
            return await _context.Students
                .Include(s => s.StuProfile)
                .Include(s => s.Tabs)
                .Include(s => s.Courses)
                .ToListAsync();
        }

        // GET student by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Stu>> GetStudent(Guid id)
        {
            var student = await _context.Students
                .Include(s => s.StuProfile)
                .Include(s => s.Tabs)
                .Include(s => s.Courses)
                .FirstOrDefaultAsync(s => s.StuId == id);

            if (student == null) return NotFound();

            return student;
        }

        // POST student
        [HttpPost]
        public async Task<ActionResult<Stu>> CreateStudent(Stu stu)
        {
            _context.Students.Add(stu);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetStudent), new { id = stu.StuId }, stu);
        }

        // PUT student
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(Guid id, Stu stu)
        {
            if (id != stu.StuId) return BadRequest();

            _context.Entry(stu).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE student
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(Guid id)
        {
            var stu = await _context.Students.FindAsync(id);
            if (stu == null) return NotFound();

            _context.Students.Remove(stu);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }

}
