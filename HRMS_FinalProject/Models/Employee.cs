using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRMS_FinalProject.Models
{
    public class Employee
    {
        
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmployeeID { get; set; }

        [Required(ErrorMessage = "First Name is required")]
        public string FirstName { get; set; } = string.Empty;

        public string MiddleName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty; 
        public string ContactEmail { get; set; } = string.Empty;  
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Required]
        public EmployeeTypes EmployeeType { get; set; }

        [Required]
        public EmployeeStatus EmployeeStatu { get; set; }

        [Required(ErrorMessage = "Hire Date is required")]
        [DataType(DataType.Date)]
        public DateTime HireDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime EmployeeEndDate { get; set; }
		[NotMapped] 
		public IFormFile? PhotoFile { get; set; }



        public string PhotoUrl { get; set; } = string.Empty;

        [Required(ErrorMessage = "Salary is required")]
        [Range(0, double.MaxValue, ErrorMessage = "Salary must be a positive number")]
        public double Salary { get; set; }

        [StringLength(50, ErrorMessage = "Reporting To can't be longer than 50 characters")]
        public string ReportingTo { get; set; } = string.Empty;

        [Required]
        public int DesignationID { get; set; }  // Foreign key for Designation

        [Required]
        public int DepartmentID { get; set; }  // Foreign key for Department

        [Required(ErrorMessage = "Phone is required")]
        [Phone(ErrorMessage = "Invalid Phone Number")]
        public string Phone { get; set; } = string.Empty;

        [Required]
        public MaritalStatus MaritalStatu { get; set; }

        public DateTime? DateOfBirth { get; set; }

        [Required(ErrorMessage = "Address is required")]
        [StringLength(200, ErrorMessage = "Address can't be longer than 200 characters")]
        public string Address { get; set; } = string.Empty;

        [StringLength(50, ErrorMessage = "City can't be longer than 50 characters")]
        public string City { get; set; } = string.Empty;

        public bool SendWelcomeEmail { get; set; }=false;
        public bool SendLoginDetails { get; set; }=false ;

        [Required]
        public Gender EmployeeGender { get; set; }

        public enum EmployeeTypes
        {
            FullTime,
            PartTime,
            Contractor,
            Intern
        }

        public enum EmployeeStatus
        {
            Active,
            Inactive,
            Terminated
        }

        public enum MaritalStatus
        {
            Single,
            Married
        }

        public enum Gender
        {
            Male,
            Female
        }

        public string FullName => $"{FirstName} {LastName}";
        public string FullNameWithDesignation => $"{FirstName} {LastName} ";

       public Designation? designation { get; set; } 

    }
}
