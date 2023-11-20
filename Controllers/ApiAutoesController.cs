using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using guran_2_2023.Data;
using guran_2_2023.Models;

namespace guran_2_2023.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiAutoesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ApiAutoesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ApiAutos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Auto>>> GetAuto()
        {
            if (_context.Auto == null)
            {
                return NotFound();
            }
            return await _context.Auto.ToListAsync();
        }

        // GET: api/ApiAutos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Auto>> GetAuto(int id)
        {
            if (_context.Auto == null)
            {
                return NotFound();
            }
            var Auto = await _context.Auto.FindAsync(id);

            if (Auto == null)
            {
                return NotFound();
            }

            return Auto;
        }

        // PUT: api/ApiAutos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAuto(int id, Auto Auto)
        {
            if (id != Auto.ID)
            {
                return BadRequest();
            }

            _context.Entry(Auto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AutoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ApiAutos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Auto>> PostAuto(Auto Auto)
        {
            if (_context.Auto == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Auto'  is null.");
            }
            _context.Auto.Add(Auto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAuto", new { id = Auto.ID }, Auto);
        }

        // DELETE: api/ApiAutos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuto(int id)
        {
            if (_context.Auto == null)
            {
                return NotFound();
            }
            var Auto = await _context.Auto.FindAsync(id);
            if (Auto == null)
            {
                return NotFound();
            }

            _context.Auto.Remove(Auto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AutoExists(int id)
        {
            return (_context.Auto?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
