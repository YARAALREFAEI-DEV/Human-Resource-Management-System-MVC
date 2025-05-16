using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HRMS_FinalProject.Data;
using HRMS_FinalProject.Models;
using HRMS_FinalProject.Extensions;

namespace HRMS_FinalProject.Areas.Employee.Controllers
{
    [Area("Employee")]
    public class ResignationsController : Controller
    {
        private readonly AppDbContext _context;

        public ResignationsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Employee/Resignations
        public async Task<IActionResult> Index()
        {
            var employee = HttpContext.Session.GetObject<HRMS_FinalProject.Models.Employee>("LoggedInEmployee");

            if (employee == null)
            {
                return NotFound(); 
            }
            var resignations = await _context.Resignation
              .Include(p => p.Employee)
              .Where(p => p.EmployeeId == employee.EmployeeID) 
              .ToListAsync();

            ViewBag.EmployeeNamePlaceholder = employee.FullName;
            return View(resignations);
        }

        // GET: Employee/Resignations/Details/5
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

        // GET: Employee/Resignations/Create
        public IActionResult Create()
        {

            ViewData["EmployeeID"] = new SelectList(_context.Employees, "EmployeeID", "FullName");

            return View();
        }

        // POST: Employee/Resignations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ResignationId,ResignationDate,Reason,Status")] Resignation resignation)
        {
            var employee = HttpContext.Session.GetObject<HRMS_FinalProject.Models.Employee>("LoggedInEmployee");

            if (employee == null)
            {
                return NotFound("Employee not logged in"); 
            }

            if (ModelState.IsValid)
            {
                resignation.EmployeeId = employee.EmployeeID;

                resignation.Status = HRMS_FinalProject.Models.Resignation.ApprovalStatus.Pending;

                _context.Add(resignation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(resignation);
        }


        // GET: Employee/Resignations/Edit/5
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

        // POST: Employee/Resignations/Edit/5
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

 
        private bool ResignationExists(int id)
        {
            return _context.Resignation.Any(e => e.ResignationId == id);
        }
    }
}
