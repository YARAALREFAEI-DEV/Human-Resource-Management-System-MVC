using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HRMS_FinalProject.Data;
using HRMS_FinalProject.Models;
using System.Security.Claims;
using HRMS_FinalProject.Extensions;

namespace HRMS_FinalProject.Areas.Employee.Controllers
{
    [Area("Employee")]
    public class GrievancesController : Controller
    {
        private readonly AppDbContext _context;

        public GrievancesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Employee/Grievances
        public async Task<IActionResult> Index()
        {
            var employee = HttpContext.Session.GetObject<HRMS_FinalProject.Models.Employee>("LoggedInEmployee");

            if (employee == null)
            {
                return NotFound(); 
            }
            var grievances = await _context.Grievance
              .Include(p => p.employee)
              .Where(p => p.EmployeeID == employee.EmployeeID) 
              .ToListAsync();

            ViewBag.EmployeeNamePlaceholder = employee.FullName;
            return View(grievances);

        }

        // GET: Employee/Grievances/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grievance = await _context.Grievance
                .Include(g => g.employee)
                .FirstOrDefaultAsync(m => m.GrievanceId == id);
            if (grievance == null)
            {
                return NotFound();
            }

            return View(grievance);
        }

        // GET: Employee/Grievances/Create
        public IActionResult Create()
        {
            var employeeId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewData["EmployeeID"] = employeeId; 
            return View();
        }

        // POST: Employee/Grievances/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GrievanceId,Description,SubmissionDate,Status")] Grievance grievance)
        {
            var employeeId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            grievance.EmployeeID = int.Parse(employeeId); 

            if (ModelState.IsValid)
            {
                _context.Add(grievance);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(grievance);
        }

        // GET: Employee/Grievances/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grievance = await _context.Grievance
                .Include(g => g.employee)
                .FirstOrDefaultAsync(m => m.GrievanceId == id);
            if (grievance == null)
            {
                return NotFound();
            }

            return View(grievance);
        }

        // POST: Employee/Grievances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var grievance = await _context.Grievance.FindAsync(id);
            if (grievance != null)
            {
                _context.Grievance.Remove(grievance);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GrievanceExists(int id)
        {
            return _context.Grievance.Any(e => e.GrievanceId == id);
        }
    }
}
