using System;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using LungMed.Data;
using LungMed.Models;
using LungMed.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LungMed.Controllers
{
    //[Authorize(Roles = "Administrator,Doctor")]
    public class UploadController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public UploadController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);

            var recordingsQuery = _context.Recording.Include(r => r.Patient)
                                                  .Where(r => r.PatientId == user.PatientId);

            recordingsQuery = recordingsQuery.OrderByDescending(r => r.DateAdded);

            var recordings = await recordingsQuery.ToListAsync();

            var viewModel = new RecordingViewModel
            {
                Recordings = recordings
            };

            if (User.IsInRole("Administrator"))
            {
                var recordingsAdmin = await _context.Recording.Include(r => r.Patient).ToListAsync();
                recordingsAdmin = recordingsAdmin.OrderByDescending(r => r.DateAdded).ToList();
                viewModel.Recordings = recordingsAdmin;

            }

            if (User.IsInRole("Lekarz"))
            {
                var recordingsDoctor = await _context.Recording.Include(r => r.Patient).Where(r => r.Patient.DoctorId == user.DoctorId).ToListAsync();
                recordingsDoctor = recordingsDoctor.OrderByDescending(r => r.DateAdded).ToList();
                viewModel.Recordings = recordingsDoctor;
            }

            return View(viewModel);
        }



        // GET: Upload/UploadFile
        [HttpGet]
        public async Task<IActionResult> UploadFile()
        {
            var user = await _userManager.GetUserAsync(User);

            var patient = await _context.Patient.FirstOrDefaultAsync(p => p.Id == user.PatientId);

            if (patient == null)
            {
                return NotFound("Patient not found for the current user.");
            }

            ViewData["PatientId"] = new SelectList(_context.Patient, "Id", "FirstName", patient.Id);
            return View();
        }

        // POST: Upload/UploadFile
        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            var user = await _userManager.GetUserAsync(User);
            var patient = await _context.Patient.FirstOrDefaultAsync(p => p.Id == user.PatientId);

            if (patient == null)
            {
                ViewBag.Message = "Patient not found.";
                return View();
            }

            try
            {
                if (file != null && file.Length > 0)
                {
                    // Ustalona nowa nazwa pliku bazująca na imieniu użytkownika
                    string currentDateTime = DateTime.Now.ToString("ddMMyyyy_HHmmss");
                    string newFileName = $"{patient.Id}_{patient.FirstName}{patient.LastName}_{currentDateTime}{Path.GetExtension(file.FileName)}";


                    // Odczytanie zawartości pliku do tablicy bajtów
                    using (var memoryStream = new MemoryStream())
                    {
                        await file.CopyToAsync(memoryStream);
                        var fileContent = memoryStream.ToArray();

                        // Tworzenie nowego obiektu UserFile
                        var userFile = new Recording
                        {
                            FileName = newFileName,
                            FileContent = fileContent,
                            PatientId = patient.Id
                        };

                        // Dodanie pliku do bazy danych
                        _context.Recording.Add(userFile);
                        await _context.SaveChangesAsync();
                    }

                    ViewBag.Message = "File Uploaded Successfully!";
                }
                else
                {
                    ViewBag.Message = "No file selected or file is empty.";
                }

                return View();
            }
            catch
            {
                ViewBag.Message = "File upload failed!";
                return View();
            }
        }

        // GET: Upload/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userFile = await _context.Recording
                .Include(uf => uf.Patient)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userFile == null)
            {
                return NotFound();
            }

            return View(userFile);
        }

        // GET: Upload/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userFile = await _context.Recording
                .Include(uf => uf.Patient)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userFile == null)
            {
                return NotFound();
            }

            return View(userFile);
        }

        // POST: Upload/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userFile = await _context.Recording.FindAsync(id);
            if (userFile != null)
            {
                _context.Recording.Remove(userFile);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool UserFileExists(int id)
        {
            return _context.Recording.Any(e => e.Id == id);
        }
    }
}
