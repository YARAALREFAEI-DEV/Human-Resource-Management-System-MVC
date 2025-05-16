namespace HRMS_FinalProject.Models
{
    using System;

    public class LeaveRequest
    {
        public int Id { get; set; } 

        public string EmployeeName { get; set; } =string.Empty;
        public string Policy { get; set; } = string.Empty; 
        public DateTime StartDate { get; set; } // Start date of the leave  
        public DateTime EndDate { get; set; } // End date of the leave  
        public int Duration { get; set; } // Duration of the leave in days  
        public int AvailableDays { get; set; } // Available leave days  
        public LeaveRequestStatus Status { get; set; } // Status of the request  
        public string Reason { get; set; } = string.Empty;// Reason for the leave  
        public DateTime? ApprovalDate { get; set; } // Date of approval  

        public enum LeaveRequestStatus
        {
            Pending,
            Approved,
            Rejected,
            Deleted
        }
    }
}
