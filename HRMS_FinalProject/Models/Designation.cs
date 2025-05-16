using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRMS_FinalProject.Models
{
    public class Designation
    {
        [Key]
        public int DesignationID { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        // Foreign key to Department
        public int DepartmentID { get; set; }

        // Navigation property to Department
        [ForeignKey("DepartmentID")]
        public Department? Department { get; set; }
    }
}
