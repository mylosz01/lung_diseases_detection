using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrzychodniaLekarska.Data;
using PrzychodniaLekarska.ViewModels;

namespace PrzychodniaLekarska.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ApplicationDbContext _context;

        public UsersController(ApplicationDbContext context, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<IdentityUser> signInManager)
        {
            _roleManager = roleManager;

            _signInManager = signInManager;

            _userManager = userManager;

            _context = context;

        }

        public async Task<ActionResult> Index()
        {
            var users = await _context.Users.ToListAsync();

            return View(users);
        }

        [HttpGet]
        public async Task<ActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(UserViewModel model)
        {
            IdentityUser user = new IdentityUser();
            user.UserName = model.UserName;
            user.NormalizedUserName = model.UserName;
            user.Email = model.Email;
            user.EmailConfirmed = true;
            user.PhoneNumber = model.PhoneNumber;
            user.PhoneNumberConfirmed = true;

            var result = await _userManager.CreateAsync(user, model.Password);

            if(result.Succeeded)
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
