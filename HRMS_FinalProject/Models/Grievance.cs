namespace HRMS_FinalProject.Models
{
    public class Grievance
    {

        public int GrievanceId { get; set; }
        public int EmployeeID { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateTime SubmissionDate { get; set; }
        public GrievanceStatus Status { get; set; }
        public virtual Employee? employee { get; set; } 
    }

    public enum GrievanceStatus
    {
        Approved,
        Pending,
      
        Rejected,
        InProcess
    }
}
