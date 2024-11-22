using Microsoft.EntityFrameworkCore;

namespace POE_Part_3.Models
{
    public class Claim
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int HoursWorked { get; set; }
        public double HourlyRate { get; set; }
        public string Notes { get; set; }
        public string? FilePath { get; set; }
        public double TotalAmount { get; set; }
        public string Status { get; set; } // Pending, Accepted, Rejected
    }
}
