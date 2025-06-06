namespace JobTrackerAPI.Models
{
    public class JobApplication
    {
        //public int Id { get; set; }
        //public string CompanyName { get; set; } = string.Empty;
        //public string Role { get; set; } = string.Empty;
        //public string Status { get; set; } = "Applied";
        //public DateTime AppliedDate { get; set; } = DateTime.UtcNow;
        //public string? Notes { get; set; }
        public Guid Id { get; set; } = Guid.NewGuid();
        public string CompanyName { get; set; } = string.Empty;
        public string Position { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string Status { get; set; } = "Applied"; // Applied, Interviewing, Offered, Rejected
        public DateTime AppliedDate { get; set; } = DateTime.UtcNow;
        public string Notes { get; set; } = string.Empty;




    }
}   
