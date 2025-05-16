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
    public class PayrollsController : Controller
    {
        private readonly AppDbContext _context;

        public PayrollsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Payrolls
        public async Task<IActionResult> Index()
        {

            var appDbContext = _context.Payroll.Include(p => p.Employee);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Admin/Payrolls/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payroll = await _context.Payroll
                .Include(p => p.Employee)
                .FirstOrDefaultAsync(m => m.PayrollId == id);
            if (payroll == null)
            {
                return NotFound();
            }

            return View(payroll);
        }

       
        

        // GET: Admin/Payrolls/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payroll = await _context.Payroll.FindAsync(id);
            if (payroll == null)
            {
                return NotFound();
            }
            ViewData["EmployeeID"] = new SelectList(_context.Employees, "EmployeeID", "FullName", payroll.EmployeeID);
            ViewData["IncrementType"] = new SelectList(Enum.GetValues(typeof(Payroll.IncrementType)));

            return View(payroll);
        }

        // POST: Admin/Payrolls/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PayrollId,EmployeeID,PayrollDate,GrossSalary,NetSalary,increment,incrementType")] Payroll payroll)
        {
            if (id != payroll.PayrollId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    payroll.CalculateIncrement();

                    // Calculate Net Salary, considering Gross Salary, Tax Deduction, and Increment
                    payroll.NetSalary = (payroll.GrossSalary + payroll.increment) - (payroll.GrossSalary * Payroll.TaxDeduction);
                    _context.Update(payroll);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PayrollExists(payroll.PayrollId))
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
            ViewData["EmployeeID"] = new SelectList(_context.Employees, "EmployeeID", "FullName", payroll.EmployeeID);
            ViewData["IncrementType"] = new SelectList(Enum.GetValues(typeof(Payroll.IncrementType)));

            return View(payroll);
        }

       

        private bool PayrollExists(int id)
        {
            return _context.Payroll.Any(e => e.PayrollId == id);
        }
    }
}
