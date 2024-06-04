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
using ModelPredict;

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

        // GET: Upload
        public IActionResult Index()
        {

            var user = _userManager.GetUserAsync(User).Result;

            var recordings = _context.Recording
                                   .Include(r => r.Patient)
                                   .Where(r => r.PatientId == user.PatientId)
                                   .ToList();

            var viewModel = new RecordingViewModel
            {
                Recordings = recordings
            };

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
                    //SEKCJA ZMIENIANA
                    // Ustalona nowa nazwa pliku bazująca na imieniu użytkownika
                    string newFileName = patient.FirstName + patient.LastName + patient.PersonalNumber + Path.GetExtension(file.FileName);
                    
                    //Tworzenie folderu do przetwarzania nagrań i ścieżki do pliku
                    ModelManager manager = new ModelManager(newFileName);

                    //Zapisanie zawartości pliku lokalnie w celu analizy przez model
                    using (var stream = new FileStream(manager.pathToAudioFile, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    //W tym miejscu zwracany jest wynik modelu dla danego nagrania
                    string modelResult = manager.GetModelResultsFromPythonScripts();

                    //Kasowanie pliku po przetworzeniu przez model
                    if(System.IO.File.Exists(manager.pathToAudioFile)) 
                    {
                        System.IO.File.Delete(manager.pathToAudioFile);
                    }
                    //SEKCJA ZMIENIANA

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
