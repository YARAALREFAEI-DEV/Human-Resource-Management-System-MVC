using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using HRMS_FinalProject.Models;
using HRMS_FinalProject.Models.ViewModels;
namespace HRMS_FinalProject.Data
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Company> Companies { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Designation> Designations { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<HRMS_FinalProject.Models.LeaveType> LeaveType { get; set; } = default!;
        public DbSet<HRMS_FinalProject.Models.LeaveRequest> LeaveRequest { get; set; } = default!;
        public DbSet<HRMS_FinalProject.Models.Payroll> Payroll { get; set; } = default!;
        public DbSet<HRMS_FinalProject.Models.Grievance> Grievance { get; set; } = default!;
        public DbSet<HRMS_FinalProject.Models.Resignation> Resignation { get; set; } = default!;
    }
}
