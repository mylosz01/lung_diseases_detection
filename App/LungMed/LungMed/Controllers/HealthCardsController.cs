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
using LungMed.ViewModels;
using Microsoft.Data.SqlClient;

namespace LungMed.Controllers
{
    [Authorize(Roles = "Administrator, Lekarz, Pacjent")]
    public class HealthCardsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HealthCardsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: HealthCards
        public async Task<IActionResult> Index(string LastNameSearch, string PersonalNumberSearch, string sortOrder)
        {
            var healthCards = from h in _context.HealthCard.Include(h => h.Patient)
                              select h;

            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
            switch (sortOrder)
            {
                case "Date":
                    healthCards = healthCards.OrderBy(s => s.Date);                 
                    break;
                case "date_desc":
                    healthCards = healthCards.OrderByDescending(s => s.Date);
                    break;
                default:
                    healthCards = healthCards.OrderByDescending(s => s.Date);
                    break;
            }


            if (User.IsInRole("Lekarz"))
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);
                healthCards = healthCards.Where(h => h.Patient.DoctorId == user.DoctorId);


            }
            if (User.IsInRole("Pacjent"))
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);
                healthCards = healthCards.Where(h => h.PatientId == user.PatientId);
            }

            

            if (!String.IsNullOrEmpty(LastNameSearch))
            {
                healthCards = healthCards.Where(s => s.Patient.LastName.Contains(LastNameSearch));
            }
            if (!String.IsNullOrEmpty(PersonalNumberSearch))
            {
                healthCards = healthCards.Where(s => s.Patient.PersonalNumber.Contains(PersonalNumberSearch));
            }


            

            var healthCardViewModel = new HealthCardViewModel
            {
                HealthCards = await healthCards.ToListAsync()
            };
            var applicationDbContext = _context.HealthCard.Include(h => h.Patient);


            return View(healthCardViewModel);
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

            var patient = await _context.Patient.FindAsync(healthCard.PatientId);

            if (healthCard == null)
            {
                return NotFound();
            }
            var doctor = await _context.Doctor.FindAsync(patient.DoctorId);
            ViewBag.DoctorDetails = $"Id: {doctor.Id} - {doctor.FirstName} {doctor.LastName}";

            return View(healthCard);
        }

        // GET: HealthCards/Create
        public async Task<IActionResult> Create()
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);

            if (User.IsInRole("Lekarz"))
            {
                var patients = _context.Patient.Where(p => p.DoctorId == user.DoctorId.Value).ToList();
                ViewData["PatientId"] = new SelectList(patients, "Id", "FullNameWithIdAndPersonal");
            }
            else
            {
                ViewData["PatientId"] = new SelectList(_context.Patient, "Id", "FullNameWithIdAndPersonal");
            }
            return View();
        }


        // POST: HealthCards/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Medicines,Diseases,Allergies,BleedingDisorders,Pregnancy,PregnancyWeek,Date,PatientId")] HealthCard healthCard)
        {
            _context.Add(healthCard);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);

            if (User.IsInRole("Lekarz"))
            {
                var patients = _context.Patient.Where(p => p.DoctorId == user.DoctorId.Value).ToList();
                ViewData["PatientId"] = new SelectList(patients, "Id", "FullNameWithIdAndPersonal");
            }
            else
            {
                ViewData["PatientId"] = new SelectList(_context.Patient, "Id", "FullNameWithIdAndPersonal");
            }

            return View(healthCard);
        }

        // GET: HealthCards/Edit/5
        public async Task<IActionResult> Edit(int? id)
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

            ViewBag.PatientDetails = $"Id: {healthCard.Patient.Id} - {healthCard.Patient.FirstName} {healthCard.Patient.LastName} {healthCard.Patient.PersonalNumber}";
            return View(healthCard);
        }

        // POST: HealthCards/Edit/5
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
            ViewBag.PatientDetails = $"Id: {healthCard.Patient.Id} - {healthCard.Patient.FirstName} {healthCard.Patient.LastName} {healthCard.Patient.PersonalNumber}";
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
            var patient = await _context.Patient.FindAsync(healthCard.PatientId);
            ViewBag.PatientDetails = $"Id: {patient.Id} - {patient.FirstName} {patient.LastName} {patient.PersonalNumber}";

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
            var patient = await _context.Patient.FindAsync(healthCard.PatientId);
            ViewBag.PatientDetails = $"Id: {patient.Id} - {patient.FirstName} {patient.LastName} {patient.PersonalNumber}";

        }

        private bool HealthCardExists(int id)
        {
            return _context.HealthCard.Any(e => e.Id == id);
        }
    }
}
