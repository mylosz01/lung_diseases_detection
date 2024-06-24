using LungMed.Data;
using LungMed.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using LungMed.Data;
using LungMed.Data.Migrations;
using LungMed.Models;
using LungMed.ViewModels;
using System.Net;
using System.Numerics;

namespace LungMed.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _context;

        public UsersController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<ApplicationUser> signInManager)
        {
            _roleManager = roleManager;

            _signInManager = signInManager;

            _userManager = userManager;

            _context = context;

        }

        public async Task<ActionResult> Index(string RoleSearchString, string LastNameSearchString)
        {
            IQueryable<string> roleQuery = from r in _context.Roles
                                           orderby r.Name
                                           select r.Name;
            var users = from u in _context.Users.Include(u => u.Role)
                        select u;

            if (!string.IsNullOrEmpty(RoleSearchString))
            {
                users = users.Where(u => u.RoleId == _context.Roles.FirstOrDefault(r => r.Name == RoleSearchString).Id);
            }
            if (!string.IsNullOrEmpty(LastNameSearchString))
            {
                users = users.Where(u => u.LastName.Contains(LastNameSearchString));
            }
            users = users.OrderBy(u => u.LastName).ThenBy(u => u.FirstName);

            var userSearchViewModel = new UserSearchViewModel
            {
                Roles = new SelectList(await roleQuery.Distinct().ToListAsync()),
                Users = await users.ToListAsync()
            };

            return View(userSearchViewModel);
        }
        //CREATE USER 
        [HttpGet]
        public async Task<ActionResult> CreateAdmin()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CreateAdmin(UserViewModel model)
        {

            var user = new ApplicationUser
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.Email,
                NormalizedUserName = model.Email,
                Email = model.Email,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                CreatedOn = DateTime.Now,
                CreatedById = _userManager.GetUserId(User)

            };


            

            var adminRole = await _roleManager.FindByNameAsync("Administrator");
            if (adminRole != null)
            {
                user.RoleId = adminRole.Id;
            }
            else
            {
                return NotFound();
            }


            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                var role = await _context.Roles.FindAsync(adminRole.Id);
                if (role != null)
                {
                    await _userManager.AddToRoleAsync(user, role.Name);
                }
                return RedirectToAction("Index");
            }


            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }


            return View(model);


        }

        //CREATE USER DOCTOR

        [HttpGet]
        public async Task<IActionResult> CreateDoctor()
        {
            await LoadDoctors();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateDoctor(UserViewModel model)
        {

            ApplicationUser user = new ApplicationUser();
            int doctorId = int.Parse(model.Doctor);
            var selectedDoctor = await _context.Doctor.FindAsync(doctorId);

            user.FirstName = selectedDoctor.FirstName;
            user.LastName = selectedDoctor.LastName;
            user.PhoneNumber = selectedDoctor.PhoneNumber;
            user.DoctorId = doctorId;
            user.UserName = model.Email;
            user.NormalizedUserName = model.Email;
            user.Email = model.Email;
            user.EmailConfirmed = true;
            user.PhoneNumberConfirmed = true;
            user.CreatedOn = DateTime.Now;
            user.CreatedById = _userManager.GetUserId(User);

            var doctorRole = await _roleManager.FindByNameAsync("Lekarz");
            if (doctorRole != null)
            {
                user.RoleId = doctorRole.Id;
            }
            else
            {
                return NotFound();
            }

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                var role = await _context.Roles.FindAsync(doctorRole.Id);
                if (role != null)
                {
                    await _userManager.AddToRoleAsync(user, role.Name);
                }
                return RedirectToAction("Index");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            await LoadDoctors(); // Ponowne załadowanie listy lekarzy
            return View(model);
            
        }

        private async Task LoadDoctors()
        {
            var doctors = await _context.Doctor.Select(d => new
            {
                Id = d.Id,
                FullNameWithId = d.FirstName + " " + d.LastName + " (Id: " + d.Id + ")"
            }).ToListAsync();

            ViewData["Doctor"] = new SelectList(doctors, "Id", "FullNameWithId");
        }

        [HttpGet]
        public async Task<ActionResult> CreatePatient()
        {
            await LoadPatients();
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CreatePatient(UserViewModel model)
        {

            ApplicationUser user = new ApplicationUser();
            int patientId = int.Parse(model.Patient);
            var selectedPatient = await _context.Patient.FindAsync(patientId);

            user.FirstName = selectedPatient.FirstName;
            user.LastName = selectedPatient.LastName;
            user.PhoneNumber = selectedPatient.PhoneNumber;
            user.PatientId = patientId;
            user.UserName = model.Email;
            user.NormalizedUserName = model.Email;
            user.Email = model.Email;
            user.EmailConfirmed = true;
            user.PhoneNumberConfirmed = true;
            user.CreatedOn = DateTime.Now;
            user.CreatedById = _userManager.GetUserId(User);

            var patientRole = await _roleManager.FindByNameAsync("Pacjent");
            if (patientRole != null)
            {
                user.RoleId = patientRole.Id;
            }
            else
            {
                return NotFound();
            }

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                var role = await _context.Roles.FindAsync(patientRole.Id);
                if (role != null)
                {
                    await _userManager.AddToRoleAsync(user, role.Name);
                }
                return RedirectToAction("Index");
            }


            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }


            await LoadPatients(); 
            return View(model);
        }

        private async Task LoadPatients()
        {
            var patients = await _context.Patient.Select(d => new
            {
                Id = d.Id,
                FullNameWithIdAndPersonal = d.FirstName + " " + d.LastName + " (Personal Number: " + d.PersonalNumber + ")"
            }).ToListAsync();

            ViewData["Patient"] = new SelectList(patients, "Id", "FullNameWithIdAndPersonal");
        }


        //[HttpGet]
        //public async Task<ActionResult> Delete(string id)
        //{
        //    var currentUser = await _userManager.GetUserAsync(User);
        //    if (currentUser.Id == id)
        //    {
        //        return RedirectToAction("Index");

        //    }

        //    var result = await _userManager.FindByIdAsync(id);
        //    if (result == null)
        //    {
        //        return NotFound();
        //    }
        //    var user = new UserViewModel()
        //    {
        //        Id = result.Id,
        //        Email = result.Email,
        //        FirstName = result.FirstName,
        //        LastName = result.LastName,
        //        Password = result.PasswordHash,
        //        UserName = result.UserName,
        //        RoleId = result.RoleId
        //    };


        //    ViewBag.RoleName = _context.Roles.FirstOrDefault(r => r.Id == result.RoleId)?.Name;

        //    return View(user);
        //}


        //[HttpPost]
        //public async Task<ActionResult> Delete(string id, UserViewModel model)
        //{
        //    var userToDelete = await _userManager.FindByIdAsync(id);
        //    if (userToDelete == null)
        //    {
        //        return NotFound();
        //    }

        //    var currentUser = await _userManager.GetUserAsync(User);
        //    if (currentUser.Id == id)
        //    {
        //        return RedirectToAction("Index");
        //    }

        //    var result = await _userManager.DeleteAsync(userToDelete);
        //    if (result.Succeeded)
        //    {
        //        return RedirectToAction("Index");
        //    }
        //    else
        //    {
        //        return View("Error");
        //    }

        //}

        [HttpGet]
        public async Task<ActionResult> Delete(string id)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser.Id == id)
            {
                TempData["ErrorMessage"] = "The user who is currently logged in cannot be deleted!";
                return RedirectToAction("Index");
            }

            var result = await _userManager.FindByIdAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            var user = new UserViewModel()
            {
                Id = result.Id,
                Email = result.Email,
                FirstName = result.FirstName,
                LastName = result.LastName,
                Password = result.PasswordHash,
                UserName = result.UserName,
                RoleId = result.RoleId
            };

            ViewBag.RoleName = _context.Roles.FirstOrDefault(r => r.Id == result.RoleId)?.Name;

            return View(user);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(string id, UserViewModel model)
        {
            var userToDelete = await _userManager.FindByIdAsync(id);
            if (userToDelete == null)
            {
                return NotFound();
            }

            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser.Id == id)
            {
                TempData["ErrorMessage"] = "The user who is currently logged in cannot be deleted!";
                return RedirectToAction("Index");
            }

            var result = await _userManager.DeleteAsync(userToDelete);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View("Error");
            }
        }




    }
}
