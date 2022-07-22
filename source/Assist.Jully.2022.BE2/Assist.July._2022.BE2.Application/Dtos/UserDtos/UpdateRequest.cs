using Assist.July._2022.BE2.Domain.Enums;

namespace Assist.July._2022.BE2.Application.Dtos.UserDtos
{
    public class UpdateRequest
    {
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public Gender Gender { get; set; }
        public string? Phone { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? Address { get; set; }
        public string? Photo { get; set; }
    }
}
