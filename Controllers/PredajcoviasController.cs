using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using guran_2_2023.Data;
using guran_2_2023.Models;

namespace guran_2_2023.Controllers
{
    public class PredajcoviasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PredajcoviasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Predajcovias
        public async Task<IActionResult> Index()
        {
            return View(await _context.Predajcovia.ToListAsync());
        }

        // GET: Predajcovias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var predajcovia = await _context.Predajcovia
                .FirstOrDefaultAsync(m => m.ID == id);
            if (predajcovia == null)
            {
                return NotFound();
            }

            return View(predajcovia);
        }

        // GET: Predajcovias/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Predajcovias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Meno,Adresa,Tel,Znacky")] Predajcovia predajcovia)
        {
            if (ModelState.IsValid)
            {
                _context.Add(predajcovia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(predajcovia);
        }

        // GET: Predajcovias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var predajcovia = await _context.Predajcovia.FindAsync(id);
            if (predajcovia == null)
            {
                return NotFound();
            }
            return View(predajcovia);
        }

        // POST: Predajcovias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Meno,Adresa,Tel,Znacky")] Predajcovia predajcovia)
        {
            if (id != predajcovia.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(predajcovia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PredajcoviaExists(predajcovia.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(predajcovia);
        }

        // GET: Predajcovias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var predajcovia = await _context.Predajcovia
                .FirstOrDefaultAsync(m => m.ID == id);
            if (predajcovia == null)
            {
                return NotFound();
            }

            return View(predajcovia);
        }

        // POST: Predajcovias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var predajcovia = await _context.Predajcovia.FindAsync(id);
            if (predajcovia != null)
            {
                _context.Predajcovia.Remove(predajcovia);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PredajcoviaExists(int id)
        {
            return _context.Predajcovia.Any(e => e.ID == id);
        }
    }
}
