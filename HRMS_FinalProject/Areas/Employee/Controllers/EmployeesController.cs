using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HRMS_FinalProject.Data;
using HRMS_FinalProject.Models;
using Microsoft.AspNetCore.Identity;

namespace HRMS_FinalProject.Areas.Employee.Controllers
{
    [Area("Employee")]
    public class EmployeesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public EmployeesController(AppDbContext context , UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager; 
        }

        // GET: Employee/Employees
        public async Task<IActionResult> Index()
        {
            var userEmail = _userManager.GetUserName(User);

            var employee = await _context.Employees
                .FirstOrDefaultAsync(p => p.Email == userEmail);

            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

       

     

        // GET: Employee/Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            ViewBag.MaritalStatusList = Enum.GetValues(typeof(HRMS_FinalProject.Models.Employee.MaritalStatus)).Cast<HRMS_FinalProject.Models.Employee.MaritalStatus>()
             .Select(e => new SelectListItem
             {
                 Value = e.ToString(),
                 Text = e.ToString()
             }).ToList();

            return View(employee);
        }

        // POST: Employee/Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        // POST: Employee/Employees/Edit/5
        // POST: Employee/Employees/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EmployeeID,FirstName,ContactEmail,Password,Phone,MaritalStatu,Address")] HRMS_FinalProject.Models.Employee employee)
        {
            if (id != employee.EmployeeID)
            {
                return NotFound();
            }

            // Fetch the existing employee from the database
            var existingEmployee = await _context.Employees.FindAsync(id);
            if (existingEmployee == null)
            {
                return NotFound();
            }

            // Get the corresponding AspNetUser for the employee
            var user = await _userManager.FindByEmailAsync(existingEmployee.ContactEmail);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "User not found.");
                return View(employee);
            }

            // Update only the specified fields
            if (ModelState.IsValid)
            {
                existingEmployee.FirstName = employee.FirstName; // Update First Name
                existingEmployee.ContactEmail = employee.ContactEmail; // Update Contact Email
                existingEmployee.Phone = employee.Phone; // Update Phone
                existingEmployee.MaritalStatu = employee.MaritalStatu; // Update Marital Status
                existingEmployee.Address = employee.Address; // Update Address

                // If the password has changed, update it in AspNetUsers
                if (!string.IsNullOrWhiteSpace(employee.Password))
                {
                    // Update password for the user
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var result = await _userManager.ResetPasswordAsync(user, token, employee.Password);
                    if (!result.Succeeded)
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                        return View(employee);
                    }
                }

                // Save changes to the database
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(existingEmployee.EmployeeID))
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

            ViewBag.MaritalStatusList = Enum.GetValues(typeof(HRMS_FinalProject.Models.Employee.MaritalStatus)).Cast<HRMS_FinalProject.Models.Employee.MaritalStatus>()
             .Select(e => new SelectListItem
             {
                 Value = e.ToString(),
                 Text = e.ToString()
             }).ToList();

            return View(employee);
        }



        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.EmployeeID == id);
        }
    }
}
