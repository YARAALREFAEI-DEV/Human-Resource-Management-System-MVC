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
    public class ResignationsController : Controller
    {
        private readonly AppDbContext _context;

        public ResignationsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Resignations
        public async Task<IActionResult> Index()
        {
            return View(await _context.Resignation.ToListAsync());
        }

        // GET: Admin/Resignations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resignation = await _context.Resignation
                .FirstOrDefaultAsync(m => m.ResignationId == id);
            if (resignation == null)
            {
                return NotFound();
            }

            return View(resignation);
        }

        // GET: Admin/Resignations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Resignations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ResignationId,EmployeeId,ResignationDate,Reason,Status")] Resignation resignation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(resignation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(resignation);
        }

        // GET: Admin/Resignations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resignation = await _context.Resignation.FindAsync(id);
            if (resignation == null)
            {
                return NotFound();
            }
            return View(resignation);
        }

        // POST: Admin/Resignations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ResignationId,EmployeeId,ResignationDate,Reason,Status")] Resignation resignation)
        {
            if (id != resignation.ResignationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(resignation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ResignationExists(resignation.ResignationId))
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
            return View(resignation);
        }

        // GET: Admin/Resignations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resignation = await _context.Resignation
                .FirstOrDefaultAsync(m => m.ResignationId == id);
            if (resignation == null)
            {
                return NotFound();
            }

            return View(resignation);
        }

        // POST: Admin/Resignations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var resignation = await _context.Resignation.FindAsync(id);
            if (resignation != null)
            {
                _context.Resignation.Remove(resignation);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ResignationExists(int id)
        {
            return _context.Resignation.Any(e => e.ResignationId == id);
        }
    }
}
