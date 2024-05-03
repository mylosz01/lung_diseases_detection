using LungMed.Data;
using LungMed.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LungMed.Data;
using LungMed.Models;
using LungMed.ViewModels;
using System.Data;

namespace LungMed.Controllers
{
    [Authorize(Roles = "Administrator")]

    public class RolesController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _context;


        public RolesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<ApplicationUser> signInManager)
        {
            _roleManager = roleManager;

            _signInManager = signInManager;

            _userManager = userManager;

            _context = context;

        }

        public async Task<ActionResult> Index()
        {
            var roles = await _context.Roles.ToListAsync();
            return View(roles);
        }

        [HttpGet]
        public async Task<ActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(RolesViewModel model)
        {
            IdentityRole role = new IdentityRole();

            role.Name = model.RoleName;


            var result = await _roleManager.CreateAsync(role);

            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View(model);
            }
        }

        [HttpGet]
        public async Task<ActionResult> Edit(string id)
        {
            var role = new RolesViewModel();
            var result = await _roleManager.FindByIdAsync(id);
            role.RoleName = result.Name;
            role.Id = result.Id;

            return View(role);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(string id, RolesViewModel model)
        {

            var checkifexist = await _roleManager.RoleExistsAsync(model.RoleName);
            if (!checkifexist)
            {
                var result = await _roleManager.FindByIdAsync(id);
                result.Name = model.RoleName;


                var finalresult = await _roleManager.UpdateAsync(result);

                if (finalresult.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(model);
                }
            }

            return View(model);

        }


        [HttpGet]
        public async Task<ActionResult> Delete(string id)
        {
            var role = new RolesViewModel();
            var result = await _roleManager.FindByIdAsync(id);
            role.RoleName = result.Name;
            role.Id = result.Id;

            return View(role);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(string id, RolesViewModel model)
        {

            var result = await _roleManager.FindByIdAsync(id);
            result.Name = model.RoleName;


            var finalresult = await _roleManager.DeleteAsync(result);

            if (finalresult.Succeeded)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View(model);
            }

        }



    }
}
