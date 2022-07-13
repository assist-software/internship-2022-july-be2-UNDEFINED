namespace Assist.Juy._2022.BE2.Domain
{
    public record UserActivity
    {
        public string? Device { get; set; }
        public string? DeviceType { get; set; }
        public string? Location { get; set; }
        public DateTime ConnectionDate { get; set; }
        public bool Status { get; set; }
    }
}
