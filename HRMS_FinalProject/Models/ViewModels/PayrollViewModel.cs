namespace HRMS_FinalProject.Models.ViewModels
{
    public class PayrollViewModel
    {
        public int? EmployeeId { get; set; }
        public IEnumerable<Payroll> Payrolls { get; set; }
    }

}
