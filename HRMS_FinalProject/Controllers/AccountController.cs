using HRMS_FinalProject.Data;
using HRMS_FinalProject.Extensions;
using HRMS_FinalProject.Models.ViewModels;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HRMS_FinalProject.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;
        private UserManager<IdentityUser> _userManager;
        private SignInManager<IdentityUser> _signInManager;
        private RoleManager<IdentityRole> _roleManager;
        public AccountController(AppDbContext context, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        // GET: Account/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    var user = await _userManager.FindByEmailAsync(model.Email);
                    if (user != null)
                    {
                        var roles = await _userManager.GetRolesAsync(user);
                        var employee = await _context.Employees.FirstOrDefaultAsync(e => e.Email == model.Email);

                        if (employee != null)
                        {
                            // Store employee in session
                            HttpContext.Session.SetObject("LoggedInEmployee", employee);
                        }

                        if (roles.Contains("Admin"))
                        {
                            return RedirectToAction("Index", "CPanel", new { area = "Admin" });
                        }
                        else if (roles.Contains("Employee"))
                        {
                            return RedirectToAction("Index", "CPanel", new { area = "Employee" });
                        }

                        else
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                }


            }

            return View(model);
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }





    }
}
