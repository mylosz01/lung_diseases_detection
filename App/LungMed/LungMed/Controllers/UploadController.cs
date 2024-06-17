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

        public async Task<IActionResult> Index(string LastNameSearch, string PersonalNumberSearch, string sortOrder)
        {
            var recordings = from r in _context.Recording.Include(r => r.Patient)
                             select r;

            if (User.IsInRole("Lekarz"))
            {
                var user = await _userManager.GetUserAsync(User);
                recordings = recordings.Where(r => r.Patient.DoctorId == user.DoctorId);
            }
            if (User.IsInRole("Pacjent"))
            {
                var user = await _userManager.GetUserAsync(User);
                recordings = recordings.Where(r => r.PatientId == user.PatientId);
            }

            if (!String.IsNullOrEmpty(LastNameSearch))
            {
                recordings = recordings.Where(s => s.Patient.LastName.Contains(LastNameSearch));
            }
            if (!String.IsNullOrEmpty(PersonalNumberSearch))
            {
                recordings = recordings.Where(s => s.Patient.PersonalNumber.Contains(PersonalNumberSearch));
            }

            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
            switch (sortOrder)
            {
                case "Date":
                    recordings = recordings.OrderBy(s => s.DateAdded);
                    break;
                case "date_desc":
                    recordings = recordings.OrderByDescending(s => s.DateAdded);
                    break;
                default:
                    recordings = recordings.OrderByDescending(s => s.DateAdded);
                    break;
            }

            var recordingViewModel = new RecordingViewModel
            {
                Recordings = await recordings.ToListAsync()
            };
            return View(recordingViewModel);
            /*
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
            */
        }



        // GET: Upload/UploadFileNOTForUser
        [HttpGet]
        public async Task<IActionResult> UploadFileNOTForUser()
        {
            return View();
        }

        // POST: Upload/UploadFileNOTForUser
        [HttpPost]
        public async Task<IActionResult> UploadFileNOTForUser(IFormFile file)
        {

            try
            {
                if (file != null && file.Length > 0)
                {
                    // Ustalona nowa nazwa pliku bazująca na imieniu użytkownika
                    string currentDateTime = DateTime.Now.ToString("ddMMyyyy_HHmmss");
                    string newFileName = $"{currentDateTime}{Path.GetExtension(file.FileName)}";

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
                    if (System.IO.File.Exists(manager.pathToAudioFile))
                    {
                        System.IO.File.Delete(manager.pathToAudioFile);
                    }

                    ViewBag.Message = "Model prediction completed successfully!\n" +
                        $" Your Results: {modelResult}";
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
                    if (System.IO.File.Exists(manager.pathToAudioFile))
                    {
                        System.IO.File.Delete(manager.pathToAudioFile);
                    }

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
                            PatientId = patient.Id,
                            ModelResult = modelResult
                        };

                        // Dodanie pliku do bazy danych
                        _context.Recording.Add(userFile);
                        await _context.SaveChangesAsync();
                    }

                    ViewBag.Message = "File Uploaded Successfully!\n" +
                        $" Results: {modelResult}";
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

            var doctor = await _context.Doctor.FindAsync(userFile.Patient.DoctorId);
            ViewBag.DoctorDetails = $"Id: {doctor.Id} - {doctor.FirstName} {doctor.LastName}";

            return View(userFile);
        }

        public async Task<IActionResult> Approve(int id)
        {
            var recording = await _context.Recording.FindAsync(id);
            if (recording == null)
            {
                return NotFound();
            }

            recording.DoctorApprove = true;
            recording.ModificationDate = DateTime.Now;

            _context.Update(recording);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Details), new { id = id });
        }

        public async Task<IActionResult> Reject(int id)
        {
            var recording = await _context.Recording.FindAsync(id);
            if (recording == null)
            {
                return NotFound();
            }

            recording.DoctorApprove = false;
            recording.ModificationDate = DateTime.Now;

            _context.Update(recording);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Details), new { id = id });
        }

        public async Task<IActionResult> DownloadFile(int id)
        {
            var recording = await _context.Recording.FindAsync(id);
            if (recording == null)
            {
                return NotFound();
            }

            return File(recording.FileContent, "application/octet-stream", recording.FileName);
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
            var patient = await _context.Patient.FindAsync(userFile.PatientId);
            ViewBag.PatientDetails = $"{patient.FullNameWithIdAndPersonal}";

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
            var patient = await _context.Patient.FindAsync(userFile.PatientId);
            ViewBag.PatientDetails = $"{patient.FullNameWithIdAndPersonal}";

            return RedirectToAction(nameof(Index));
        }

        private bool UserFileExists(int id)
        {
            return _context.Recording.Any(e => e.Id == id);
        }
    }
}
