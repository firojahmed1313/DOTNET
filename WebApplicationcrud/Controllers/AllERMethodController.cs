using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplicationcrud.Data;
using WebApplicationcrud.Models.Entities;

namespace WebApplicationcrud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AllERMethodController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AllERMethodController(AppDbContext appDbContext)
        {
            this._context = appDbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AllValidation>>> GetDatas()
        {
            return await _context.AllValidations.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<AllValidation>> Create(AllValidation data)
        {
            _context.AllValidations.Add(data);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetDatas), data);
        }
    }
}
