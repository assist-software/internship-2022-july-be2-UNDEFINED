namespace Assist.July._2022.BE2.Domain.Entities
{
    public class Message
    {
        public Guid SenderId { get; set; }
        public Guid ReceiverId { get; set; }
        public Guid ListingId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string? Content { get; set; }
        public bool ViewStatus { get; set; }
    }
}
