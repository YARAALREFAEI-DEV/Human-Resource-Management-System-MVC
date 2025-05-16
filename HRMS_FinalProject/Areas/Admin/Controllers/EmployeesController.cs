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

namespace HRMS_FinalProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class EmployeesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public EmployeesController(AppDbContext context, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;

        }


        // GET: Admin/Employees
        public async Task<IActionResult> Index()
        {
            return View(await _context.Employees.ToListAsync());
        }

        // GET: Admin/Employees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .FirstOrDefaultAsync(m => m.EmployeeID == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: Admin/Employees/Create
        public IActionResult Create()
        {
            ViewBag.MaritalStatusList = Enum.GetValues(typeof(HRMS_FinalProject.Models.Employee.MaritalStatus)).Cast<HRMS_FinalProject.Models.Employee.MaritalStatus>()
               .Select(e => new SelectListItem
               {
                   Value = e.ToString(),
                   Text = e.ToString()
               }).ToList();


            ViewBag.EmployeeTypes = Enum.GetValues(typeof(HRMS_FinalProject.Models.Employee.EmployeeTypes)).Cast<HRMS_FinalProject.Models.Employee.EmployeeTypes>()
                                        .Select(e => new SelectListItem
                                        {
                                            Value = e.ToString(),
                                            Text = e.ToString()
                                        }).ToList();

            ViewBag.EmployeeStatuses = Enum.GetValues(typeof(HRMS_FinalProject.Models.Employee.EmployeeStatus)).Cast<HRMS_FinalProject.Models.Employee.EmployeeStatus>()
                                            .Select(e => new SelectListItem
                                            {
                                                Value = e.ToString(),
                                                Text = e.ToString()
                                            }).ToList();

            ViewBag.Genders = Enum.GetValues(typeof(HRMS_FinalProject.Models.Employee.Gender)).Cast<HRMS_FinalProject.Models.Employee.Gender>()
                                  .Select(e => new SelectListItem
                                  {
                                      Value = e.ToString(),
                                      Text = e.ToString()
                                  }).ToList();

            var employees = _context.Employees
      .Select(e => new { e.EmployeeID, FullNameWithDesignation = $"{e.FirstName} {e.LastName} - {(e.designation.Title ?? "No Designation")}" })
      .ToList();

            ViewBag.Employees = new SelectList(employees, "EmployeeID", "FullNameWithDesignation");
            return View();
        }

        // POST: Admin/Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmployeeID,FirstName,MiddleName,LastName,EmployeeGender,Email,ContactEmail,Password,EmployeeType,EmployeeStatu,HireDate,EmployeeEndDate,Salary,ReportingTo,DepartmentID,DesignationID,Phone,Address,City,MaritalStatu,DateOfBirth,PhotoUrl,ReportingTo,SendWelcomeEmail,SendLoginDetails")] HRMS_FinalProject.Models.Employee employee)
        {
            if (ModelState.IsValid)
            {

                // Create a new IdentityUser based on the employee's email
                var user = new IdentityUser
                {
                    UserName = employee.Email,
                    Email = employee.Email,
                    EmailConfirmed = true // Assuming you want to confirm the email
                };

                // Create the user with the specified password
                var result = await _userManager.CreateAsync(user, employee.Password);

                if (result.Succeeded)
                {
                    // Assign the "Employee" role to the newly created user
                    await _userManager.AddToRoleAsync(user, "Employee");

                    // Add the employee record to the database context
                    _context.Add(employee);
                    await _context.SaveChangesAsync();

                    // Redirect to the Index action in the Employee area after successful creation
                    return RedirectToAction("Index", "Employees", new { area = "Admin" });
                }
                else
                {
                    // If there were errors in creating the user, add them to the ModelState
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }

            // If we got this far, something failed; return the employee view with current data
            return View(employee);
        }


        // GET: Admin/Employees/Edit/5
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

            // Load ViewBag data for dropdowns
            ViewBag.MaritalStatusList = Enum.GetValues(typeof(HRMS_FinalProject.Models.Employee.MaritalStatus))
                .Cast<HRMS_FinalProject.Models.Employee.MaritalStatus>()
                .Select(e => new SelectListItem
                {
                    Value = e.ToString(),
                    Text = e.ToString()
                }).ToList();

            ViewBag.EmployeeTypes = Enum.GetValues(typeof(HRMS_FinalProject.Models.Employee.EmployeeTypes))
                .Cast<HRMS_FinalProject.Models.Employee.EmployeeTypes>()
                .Select(e => new SelectListItem
                {
                    Value = e.ToString(),
                    Text = e.ToString()
                }).ToList();

            ViewBag.EmployeeStatuses = Enum.GetValues(typeof(HRMS_FinalProject.Models.Employee.EmployeeStatus))
                .Cast<HRMS_FinalProject.Models.Employee.EmployeeStatus>()
                .Select(e => new SelectListItem
                {
                    Value = e.ToString(),
                    Text = e.ToString()
                }).ToList();

            ViewBag.Genders = Enum.GetValues(typeof(HRMS_FinalProject.Models.Employee.Gender))
                .Cast<HRMS_FinalProject.Models.Employee.Gender>()
                .Select(e => new SelectListItem
                {
                    Value = e.ToString(),
                    Text = e.ToString()
                }).ToList();
            var employees = _context.Employees
    .Select(e => new { e.EmployeeID, FullNameWithDesignation = $"{e.FirstName} {e.LastName} - {(e.designation.Title ?? "No Designation")}" })
    .ToList();

            ViewBag.Employees = new SelectList(employees, "EmployeeID", "FullNameWithDesignation");
            return View(employee);
        }


        // POST: Admin/Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EmployeeID,FirstName,MiddleName,LastName,Email,ContactEmail,EmployeeType,EmployeeStatu,HireDate,EmployeeEndDate,PhotoUrl,Salary,ReportingTo,DepartmentID,DesignationID,Phone,MaritalStatu,DateOfBirth,Address,City,SendWelcomeEmail,SendLoginDetails,EmployeeGender")] HRMS_FinalProject.Models.Employee employee)
        {
            if (id != employee.EmployeeID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.EmployeeID))
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
            return View(employee);
        }

        // GET: Admin/Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .FirstOrDefaultAsync(m => m.EmployeeID == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Admin/Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.EmployeeID == id);
        }
    }
}
