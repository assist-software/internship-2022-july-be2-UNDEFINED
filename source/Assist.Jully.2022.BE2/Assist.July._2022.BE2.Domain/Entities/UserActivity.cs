namespace Assist.July._2022.BE2.Domain.Entities
{
    public record UserActivity
    {
        public Guid Id { get; set; }
        public ICollection<User>? Users { get; set; }
        public string? Device { get; set; }
        public string? DeviceType { get; set; }
        public string? Location { get; set; }
        public DateTime ConnectionDate { get; set; }

        public bool Status { get; set; }
    }
}
