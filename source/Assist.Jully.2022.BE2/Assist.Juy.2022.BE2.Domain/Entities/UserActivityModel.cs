namespace Assist.Juy_2022.BE2.Domain
{
    public record UserActivityModel
    {
        string Device { get; set; } 
        string DeviceType { get; set; }
        string Location { get; set; }
        DateTime ConnectionDate { get; set; }
        enum Status { };
    }
}
