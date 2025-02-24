namespace TestAssignment.Models
{
    public class BlockedAttempt
    {
        public string? IP { get; set; }
        public string? CountryCode { get; set; }
        public DateTime Timestamp { get; set; }
        public string? UserAgent { get; set; }
    }
}
