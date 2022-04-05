using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SacramentPlanner.Models;

namespace SacramentPlanner.Controllers
{
    public class SundaysController : Controller
    {
        private readonly SacramentPlannerContext _context;

        public SundaysController(SacramentPlannerContext context)
        {
            _context = context;
        }

        // GET: Sundays
        public async Task<IActionResult> Index(string searchString)
        {
            var sunday = from s in _context.Sunday select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                sunday = sunday.Where(s => s.Conductor.Contains(searchString));
            }
            
            foreach(var s in sunday)
            {
                s.Speakers = _context.Speaker.Where(x => x.Sunday.Id == s.Id).ToList();
            }
            return View(await sunday.ToListAsync());
        }

        // GET: Sundays/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sunday = await _context.Sunday
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sunday == null)
            {
                return NotFound();
            }

            return View(sunday);
        }

        // GET: Sundays/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Sundays/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Date,Conductor,OpeningHymn,ClosingHymn,SacramentHymn,IntermediateSong,OpeningPrayer,ClosingPrayer,Speakers")] Sunday sunday)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sunday);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sunday);
        }

        // GET: Sundays/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sunday = await _context.Sunday.FindAsync(id);
            if (sunday == null)
            {
                return NotFound();
            }
            return View(sunday);
        }

        // POST: Sundays/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Date,Conductor,OpeningHymn,ClosingHymn,SacramentHymn,IntermediateSong,OpeningPrayer,ClosingPrayer")] Sunday sunday)
        {
            if (id != sunday.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sunday);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SundayExists(sunday.Id))
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
            return View(sunday);
        }

        // GET: Sundays/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sunday = await _context.Sunday
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sunday == null)
            {
                return NotFound();
            }

            return View(sunday);
        }

        // POST: Sundays/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sunday = await _context.Sunday.FindAsync(id);
            _context.Sunday.Remove(sunday);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SundayExists(int id)
        {
            return _context.Sunday.Any(e => e.Id == id);
        }
    }
}
