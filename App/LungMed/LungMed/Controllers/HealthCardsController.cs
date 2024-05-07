using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LungMed.Data;
using LungMed.Models;
using Microsoft.AspNetCore.Authorization;

namespace LungMed.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class HealthCardsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HealthCardsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: HealthCards
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.HealthCard.Include(h => h.Patient);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: HealthCards/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var healthCard = await _context.HealthCard
                .Include(h => h.Patient)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (healthCard == null)
            {
                return NotFound();
            }

            return View(healthCard);
        }

        // GET: HealthCards/Create
        public IActionResult Create()
        {
            ViewData["PatientId"] = new SelectList(_context.Patient, "Id", "FullNameWithIdAndPersonal");
            return View();
        }

        // POST: HealthCards/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Medicines,Diseases,Allergies,BleedingDisorders,Pregnancy,PregnancyWeek,Date,PatientId")] HealthCard healthCard)
        {
            if (ModelState.IsValid)
            {
                _context.Add(healthCard);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PatientId"] = new SelectList(_context.Patient, "Id", "FullNameWithIdAndPersonal");
            return View(healthCard);
        }

        // GET: HealthCards/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var healthCard = await _context.HealthCard.FindAsync(id);
            if (healthCard == null)
            {
                return NotFound();
            }
            ViewData["PatientId"] = new SelectList(_context.Patient, "Id", "Id", healthCard.PatientId);
            return View(healthCard);
        }

        // POST: HealthCards/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Medicines,Diseases,Allergies,BleedingDisorders,Pregnancy,PregnancyWeek,Date,PatientId")] HealthCard healthCard)
        {
            if (id != healthCard.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(healthCard);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HealthCardExists(healthCard.Id))
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
            ViewData["PatientId"] = new SelectList(_context.Patient, "Id", "Id", healthCard.PatientId);
            return View(healthCard);
        }

        // GET: HealthCards/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var healthCard = await _context.HealthCard
                .Include(h => h.Patient)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (healthCard == null)
            {
                return NotFound();
            }

            return View(healthCard);
        }

        // POST: HealthCards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var healthCard = await _context.HealthCard.FindAsync(id);
            if (healthCard != null)
            {
                _context.HealthCard.Remove(healthCard);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HealthCardExists(int id)
        {
            return _context.HealthCard.Any(e => e.Id == id);
        }
    }
}
