using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRMS_FinalProject.Models
{
    public class Department
    {
        [Key]
        public int DepartmentID { get; set; }

        [Required]
        public string DepartmentTitle { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public int? DepartmentManagerID { get; set; }

        
        [ForeignKey("DepartmentManagerID")]
        public virtual Employee? DepartmentManager { get; set; }

        public IEnumerable<string> EmployeeNamesWithDesignations
        {
            get
            {
                return Employees?.Select(e => e.FullNameWithDesignation) ?? Enumerable.Empty<string>();
            }
        }
        // One-to-many relationship with Designation
        public ICollection<Designation>? Designations { get; set; }

        // One-to-many relationship with Employees
        public virtual ICollection<Employee>? Employees { get; set; }
    }
}
