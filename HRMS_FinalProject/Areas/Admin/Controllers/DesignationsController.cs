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
    public class DesignationsController : Controller
    {
        private readonly AppDbContext _context;

        public DesignationsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Designations
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Designations.Include(d => d.Department);
            return View(await appDbContext.ToListAsync());
        }

    

        // GET: Admin/Designations/Create
        public IActionResult Create()
        {
            ViewData["DepartmentID"] = new SelectList(_context.Departments, "DepartmentID", "DepartmentTitle");
            return View();
        }

        // POST: Admin/Designations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DesignationID,Title,DepartmentID")] Designation designation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(designation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DepartmentID"] = new SelectList(_context.Departments, "DepartmentID", "DepartmentTitle", designation.DepartmentID);
            return View(designation);
        }

        // GET: Admin/Designations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var designation = await _context.Designations.FindAsync(id);
            if (designation == null)
            {
                return NotFound();
            }
            ViewData["DepartmentID"] = new SelectList(_context.Departments, "DepartmentID", "DepartmentTitle", designation.DepartmentID);
            return View(designation);
        }

        // POST: Admin/Designations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DesignationID,Title,DepartmentID")] Designation designation)
        {
            if (id != designation.DesignationID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(designation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DesignationExists(designation.DesignationID))
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
            ViewData["DepartmentID"] = new SelectList(_context.Departments, "DepartmentID", "DepartmentTitle", designation.DepartmentID);
            return View(designation);
        }

        // GET: Admin/Designations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var designation = await _context.Designations
                .Include(d => d.Department)
                .FirstOrDefaultAsync(m => m.DesignationID == id);
            if (designation == null)
            {
                return NotFound();
            }

            return View(designation);
        }

        // POST: Admin/Designations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var designation = await _context.Designations.FindAsync(id);
            if (designation != null)
            {
                _context.Designations.Remove(designation);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DesignationExists(int id)
        {
            return _context.Designations.Any(e => e.DesignationID == id);
        }
    }
}
