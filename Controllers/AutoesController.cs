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
    public class AutoesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _appEnvironment; //aby sa dali ukladat obrazky

        public AutoesController(ApplicationDbContext context, IWebHostEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;
        }

        // GET: Autoes
        public async Task<IActionResult> Index()
        {
            return _context.Auto != null ?
                        View(await _context.Auto.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.Auto'  is null.");
        }

        // GET: Autoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Auto == null)
            {
                return NotFound();
            }

            var Auto = await _context.Auto
                .FirstOrDefaultAsync(m => m.ID == id);
            if (Auto == null)
            {
                return NotFound();
            }

            return View(Auto);
        }

        // GET: Autoes/
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Znacka,Cena,Fotografia")] Auto auto)
        {
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                int nbImg = 0;
                foreach (var Image in files)
                {
                    if (Image != null && Image.Length > 0)
                    {
                        var file = Image;
                        var uploads = Path.Combine(_appEnvironment.WebRootPath, "uploads", "images");
                        if (file.Length > 0)
                        {
                            var fileName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(file.FileName);
                            using (var fileStream = new FileStream(Path.Combine(uploads, fileName), FileMode.Create))
                            {
                                await file.CopyToAsync(fileStream);
                                if (nbImg == 0)
                                { auto.Fotografia = fileName; }
                                else
                                { auto.Fotografia = auto.Fotografia + "/" + fileName; }
                                nbImg++;
                            }
                        }
                    }
                }

                _context.Add(auto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(auto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Znacka,Cena,Fotografia")] Auto auto)
        {
            if (id != auto.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var files = HttpContext.Request.Form.Files;
                    int nbImg = 0;
                    foreach (var Image in files)
                    {
                        if (Image != null && Image.Length > 0)
                        {
                            var file = Image;
                            var uploads = Path.Combine(_appEnvironment.WebRootPath, "uploads", "images");
                            if (file.Length > 0)
                            {
                                var fileName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(file.FileName);
                                using (var fileStream = new FileStream(Path.Combine(uploads, fileName), FileMode.Create))
                                {
                                    await file.CopyToAsync(fileStream);
                                    if (nbImg == 0)
                                    { auto.Fotografia = fileName; }
                                    else
                                    { auto.Fotografia = auto.Fotografia + "/" + fileName; }
                                    nbImg++;
                                }
                            }
                        }
                    }

                    _context.Update(auto);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AutoExists(auto.ID))
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
            return View(auto);
        }

        // GET: Autoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Auto == null)
            {
                return NotFound();
            }

            var Auto = await _context.Auto
                .FirstOrDefaultAsync(m => m.ID == id);
            if (Auto == null)
            {
                return NotFound();
            }

            return View(Auto);
        }

        // POST: Autoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Auto == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Auto'  is null.");
            }
            var Auto = await _context.Auto.FindAsync(id);
            if (Auto != null)
            {
                _context.Auto.Remove(Auto);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AutoExists(int id)
        {
            return (_context.Auto?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
