using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HRMS_FinalProject.Data;
using HRMS_FinalProject.Models;

namespace HRMS_FinalProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class GrievancesController : Controller
    {
        private readonly AppDbContext _context;

        public GrievancesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Grievances
        public async Task<IActionResult> Index()
        {
            var grievances = await _context.Grievance
                .Include(g => g.employee) // Ensure the related employee is loaded
                .ToListAsync();
            return View(grievances);
        }

        // GET: Admin/Grievances/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grievance = await _context.Grievance
                .FirstOrDefaultAsync(m => m.GrievanceId == id);
            if (grievance == null)
            {
                return NotFound();
            }
            var employees = _context.Employees
     .Select(e => new { e.EmployeeID, FullNameWithDesignation = $"{e.FirstName} {e.LastName} - {(e.designation.Title ?? "No Designation")}" })
     .ToList();

            ViewBag.Employees = new SelectList(employees, "EmployeeID", "FullNameWithDesignation");
            return View(grievance);
        }

        

        // GET: Admin/Grievances/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grievance = await _context.Grievance.FindAsync(id);
            if (grievance == null)
            {
                return NotFound();

            }
            ViewBag.GrievanceStatusList = Enum.GetValues(typeof(HRMS_FinalProject.Models.GrievanceStatus)).Cast<HRMS_FinalProject.Models.GrievanceStatus>()
         .Select(e => new SelectListItem
         {
             Value = e.ToString(),
             Text = e.ToString()
         }).ToList();
            var employees = _context.Employees
         .Select(e => new { e.EmployeeID, FullNameWithDesignation = $"{e.FirstName} {e.LastName} - {(e.designation.Title ?? "No Designation")}" })
         .ToList();

            ViewBag.Employees = _context.Employees
     .Select(e => new SelectListItem
     {
         Value = e.EmployeeID.ToString(),
         Text = e.FullName // Replace "Name" with the correct property for employee name
     })
     .ToList();

            ViewBag.Employees = new SelectList(employees, "EmployeeID", "FullNameWithDesignation");
            return View(grievance);
        }

        // POST: Admin/Grievances/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GrievanceId,EmployeeID,Description,SubmissionDate,Status")] Grievance grievance)
        {
            if (id != grievance.GrievanceId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(grievance);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GrievanceExists(grievance.GrievanceId))
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
            return View(grievance);
        }

        // GET: Admin/Grievances/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grievance = await _context.Grievance
                .FirstOrDefaultAsync(m => m.GrievanceId == id);
            if (grievance == null)
            {
                return NotFound();
            }

            return View(grievance);
        }

        // POST: Admin/Grievances/Delete/5
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
