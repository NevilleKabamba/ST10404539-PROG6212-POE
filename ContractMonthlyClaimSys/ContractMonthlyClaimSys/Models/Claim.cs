using System;
using System.Collections.Generic;

namespace ContractMonthlyClaimSys.Models
{
    public class Claim
    {
        public int Id { get; set; }
        public string LecturerName { get; set; }
        public int HoursWorked { get; set; }
        public decimal HourlyRate { get; set; }
        public string Notes { get; set; }
        public decimal TotalAmount => HoursWorked * HourlyRate;
        public string Status { get; set; } = "Pending"; // Default status
        public DateTime SubmittedDate { get; set; } = DateTime.Now;

        // Navigation property
        public List<UploadedFile> UploadedFiles { get; set; }
    }
}
