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

        public async Task<ActionResult> Index()
        {

            var users = await _context.Users.Include(x => x.Role).ToListAsync();

            return View(users);
        }
        //CREATE USER 
        [HttpGet]
        public async Task<ActionResult> Create()
        {

            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Name");


            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(UserViewModel model)
        {
            ApplicationUser user = new ApplicationUser();


            user.FirstName = model.FirstName;
            user.LastName = model.LastName;


            user.UserName = model.Email;
            user.NormalizedUserName = model.Email;
            user.Email = model.Email;
            user.EmailConfirmed = true;
            user.PhoneNumberConfirmed = true;
            user.CreatedOn = DateTime.Now;
            user.CreatedById = "Admin";
            user.RoleId = model.RoleId;


            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, _context.Roles.FirstOrDefault(r => r.Id == model.RoleId).Name);

                return RedirectToAction("Index");
            }
            else
            {
                foreach (var error in result.Errors)
                {

                    if (error.Code == "DuplicateUserName")
                    {
                        ModelState.AddModelError(string.Empty, "Konto o podanym adresie email już istnieje.");
                        break;
                    }
                }

                return View(model);
            }

            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Name", model.RoleId);

        }

        //CREATE USER DOCTOR

        [HttpGet]
        public async Task<ActionResult> CreateDoctor()
        {

            var doctors = await _context.Doctor.Select(d => new Doctor
            {
                Id = d.Id,
                FirstName = d.FirstName,
                LastName = d.LastName,
                PhoneNumber = d.PhoneNumber
            }).ToListAsync();

            ViewData["Doctor"] = new SelectList(doctors, "Id", "FullNameWithId");
            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Name");


            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CreateDoctor(UserViewModel model)
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
            user.CreatedById = "Admin";
            user.RoleId = model.RoleId;


            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, _context.Roles.FirstOrDefault(r => r.Id == model.RoleId).Name);

                return RedirectToAction("Index");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                
                    if (error.Code == "DuplicateUserName")
                    {
                        ModelState.AddModelError(string.Empty, "Konto o podanym adresie email już istnieje.");
                        break;
                    }
                }

                return View(model);
            }

            var doctors = await _context.Doctor.Select(d => new Doctor
            {
                Id = d.Id,
                FirstName = d.FirstName,
                LastName = d.LastName,
                PhoneNumber = d.PhoneNumber
            }).ToListAsync();

            ViewData["Doctor"] = new SelectList(doctors, "Id", "FullNameWithId");
            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Name", model.RoleId);

        }

        //CREATE USER PATIENT
        [HttpGet]
        public async Task<ActionResult> CreatePatient()
        {

            var patients = await _context.Patient.Select(d => new Patient
            {
                Id = d.Id,
                FirstName = d.FirstName,
                LastName = d.LastName,
                PhoneNumber = d.PhoneNumber,
                PersonalNumber = d.PersonalNumber
            }).ToListAsync();

            ViewData["Patient"] = new SelectList(patients, "Id", "FullNameWithIdAndPersonal");
            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Name");


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
            user.CreatedById = "Admin";
            user.RoleId = model.RoleId;


            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, _context.Roles.FirstOrDefault(r => r.Id == model.RoleId).Name);

                return RedirectToAction("Index");
            }
            else
            {
                foreach (var error in result.Errors)
                {

                    if (error.Code == "DuplicateUserName")
                    {
                        ModelState.AddModelError(string.Empty, "Konto o podanym adresie email już istnieje.");
                        break;
                    }
                }

                return View(model);
            }

            var patients = await _context.Patient.Select(d => new Patient
            {
                Id = d.Id,
                FirstName = d.FirstName,
                LastName = d.LastName,
                PhoneNumber = d.PhoneNumber,
                PersonalNumber = d.PersonalNumber
            }).ToListAsync();

            ViewData["Patient"] = new SelectList(patients, "Id", "FullNameWithIdAndPersonal");
            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Name", model.RoleId);

        }

        [HttpGet]
        public async Task<ActionResult> Delete(string id)
        {
            var user = new UserViewModel();
            var result = await _userManager.FindByIdAsync(id);

            user.Id = result.Id;
            user.Email = result.Email;
            user.FirstName = result.FirstName;
            user.LastName = result.LastName;
            user.Password = result.PasswordHash;
            user.UserName = result.UserName;
            user.RoleId = result.RoleId;

            ViewBag.RoleName = _context.Roles.FirstOrDefault(r => r.Id == result.RoleId)?.Name;

            return View(user);
        }


        [HttpPost]
        public async Task<ActionResult> Delete(string id, UserViewModel model)
        {

            var user = await _userManager.FindByIdAsync(id);
            user.FirstName = model.FirstName;

            var result = await _userManager.DeleteAsync(user);
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
