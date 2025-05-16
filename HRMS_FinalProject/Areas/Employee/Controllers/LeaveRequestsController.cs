using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HRMS_FinalProject.Data;
using HRMS_FinalProject.Models;
using static HRMS_FinalProject.Models.LeaveRequest;

namespace HRMS_FinalProject.Areas.Employee.Controllers
{
    [Area("Employee")]
    public class LeaveRequestsController : Controller
    {
        private readonly AppDbContext _context;

        public LeaveRequestsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Employee/LeaveRequests
        public async Task<IActionResult> Index()
        {
            return View(await _context.LeaveRequest.ToListAsync());
        }

        // GET: Employee/LeaveRequests/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leaveRequest = await _context.LeaveRequest
                .FirstOrDefaultAsync(m => m.Id == id);
            if (leaveRequest == null)
            {
                return NotFound();
            }

            return View(leaveRequest);
        }

        // GET: Employee/LeaveRequests/Create
        public async Task<IActionResult> Create()
        {
            // Fetch leave types from the database
            var leaveTypes = await _context.LeaveType.ToListAsync();

            // Assign the leave types to ViewBag
            ViewBag.LeaveTypes = leaveTypes;

            // Create a list of SelectListItem from LeaveRequestStatus enum
            ViewBag.LeaveRequestStatuses = Enum.GetValues(typeof(LeaveRequestStatus))
                .Cast<LeaveRequestStatus>()
                .Select(status => new SelectListItem
                {
                    Value = status.ToString(),
                    Text = status.ToString()
                }).ToList();

            // Assign the leave types to ViewBag
            ViewBag.LeaveTypes = leaveTypes;
            return View();
        }

        // POST: Employee/LeaveRequests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Policy,StartDate,EndDate,Duration,AvailableDays,Status,Reason")] LeaveRequest leaveRequest)
        {
            if (ModelState.IsValid)
            {
                _context.Add(leaveRequest);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(leaveRequest);
        }

       

        // GET: Employee/LeaveRequests/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leaveRequest = await _context.LeaveRequest
                .FirstOrDefaultAsync(m => m.Id == id);
            if (leaveRequest == null)
            {
                return NotFound();
            }

            return View(leaveRequest);
        }

        // POST: Employee/LeaveRequests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var leaveRequest = await _context.LeaveRequest.FindAsync(id);
            if (leaveRequest != null)
            {
                _context.LeaveRequest.Remove(leaveRequest);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LeaveRequestExists(int id)
        {
            return _context.LeaveRequest.Any(e => e.Id == id);
        }
    }
}
