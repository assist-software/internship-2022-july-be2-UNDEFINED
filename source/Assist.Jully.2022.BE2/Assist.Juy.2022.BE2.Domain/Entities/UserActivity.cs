namespace Assist.Juy._2022.BE2.Domain
{
    public record UserActivity
    {
        public string? Device { get; set; } 
        string? DeviceType { get; set; }
        string? Location { get; set; }
        DateTime ConnectionDate { get; set; }
        public bool Status { get; set; }
    }
}
