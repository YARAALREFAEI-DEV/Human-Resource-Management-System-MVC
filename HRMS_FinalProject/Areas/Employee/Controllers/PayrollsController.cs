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
    public class PayrollsController : Controller
    {
        private readonly AppDbContext _context;

        public PayrollsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Employee/Payrolls
        public async Task<IActionResult> Index()
        {
            var employee = HttpContext.Session.GetObject<HRMS_FinalProject.Models.Employee>("LoggedInEmployee"); 

            if (employee == null)
            {
                return NotFound(); 
            }

            // Fetch payroll records for the logged-in employee
            var payrolls = await _context.Payroll
                .Include(p => p.Employee)
                .Where(p => p.EmployeeID == employee.EmployeeID) 
                .ToListAsync();

            return View(payrolls);
        }


        // GET: Employee/Payrolls/Details/5
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

       

        private bool PayrollExists(int id)
        {
            return _context.Payroll.Any(e => e.PayrollId == id);
        }
    }
}
