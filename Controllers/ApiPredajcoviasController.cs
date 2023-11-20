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
    public class ApiPredajcoviasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ApiPredajcoviasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ApiPredajcovias
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Predajcovia>>> GetPredajcovia()
        {
            if (_context.Predajcovia == null)
            {
                return NotFound();
            }
            return await _context.Predajcovia.ToListAsync();
        }

        // GET: api/ApiPredajcovias/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Predajcovia>> GetPredajcovia(int id)
        {
            if (_context.Predajcovia == null)
            {
                return NotFound();
            }
            var Predajcovia = await _context.Predajcovia.FindAsync(id);

            if (Predajcovia == null)
            {
                return NotFound();
            }

            return Predajcovia;
        }

        // PUT: api/ApiPredajcovias/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPredajcovia(int id, Predajcovia Predajcovia)
        {
            if (id != Predajcovia.ID)
            {
                return BadRequest();
            }

            _context.Entry(Predajcovia).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PredajcoviaExists(id))
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

        // POST: api/ApiPredajcovias
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Predajcovia>> PostPredajcovia(Predajcovia Predajcovia)
        {
            if (_context.Predajcovia == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Predajcovia'  is null.");
            }
            _context.Predajcovia.Add(Predajcovia);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPredajcovia", new { id = Predajcovia.ID }, Predajcovia);
        }

        // DELETE: api/ApiPredajcovias/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePredajcovia(int id)
        {
            if (_context.Predajcovia == null)
            {
                return NotFound();
            }
            var Predajcovia = await _context.Predajcovia.FindAsync(id);
            if (Predajcovia == null)
            {
                return NotFound();
            }

            _context.Predajcovia.Remove(Predajcovia);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PredajcoviaExists(int id)
        {
            return (_context.Predajcovia?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
