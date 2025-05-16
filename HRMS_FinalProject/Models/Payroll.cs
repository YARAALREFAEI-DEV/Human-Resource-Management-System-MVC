
using System;
using System.ComponentModel.DataAnnotations;

namespace HRMS_FinalProject.Models
{
    public class Payroll
    {
        [Key]
        public int PayrollId { get; set; }

        public int EmployeeID { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime PayrollDate { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Gross Salary must be a positive value.")]
        public double GrossSalary { get; set; }


        public double NetSalary { get; set; }
        public  double increment { get; set; }


        public const double TaxDeduction = 0.05;

        public IncrementType incrementType { get; set; }
        public enum IncrementType
        {
            Performance,     
            Promotion,       
            CostOfLiving,     
            ServiceYears,
            NoIncrement
        }

        public virtual Employee? Employee { get; set; }
        public void CalculateIncrement()
        {
            switch (incrementType)
            {
                case IncrementType.Performance:
                    increment = GrossSalary * 0.10; 
                    break;
                case IncrementType.Promotion:
                    increment = GrossSalary * 0.15; 
                    break;
                case IncrementType.CostOfLiving:
                    increment = GrossSalary * 0.05; 
                    break;
                case IncrementType.ServiceYears:
                    increment = GrossSalary * 0.07;
                    break;
                case IncrementType.NoIncrement:
                default:
                    increment = 0;
                    break;
            }
        }
    }

}
