namespace HRMS_FinalProject.Models
{
    public class Resignation
    {
        public int ResignationId { get; set; }
        public int EmployeeId { get; set; }
        public DateTime ResignationDate { get; set; }
        public string Reason { get; set; } = string.Empty;
        public ApprovalStatus Status { get; set; }
        public virtual Employee? Employee { get; set; }

        public enum ApprovalStatus
        {
            Pending,
            Approved,
            Rejected
        }

    }
}
