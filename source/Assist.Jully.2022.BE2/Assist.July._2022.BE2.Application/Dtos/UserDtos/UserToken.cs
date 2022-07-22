using Assist.July._2022.BE2.Domain.Enums;

namespace Assist.July._2022.BE2.Application.Dtos.UserDtos
{
    public class UserToken
    {
        public Guid id { get; set; }
        public string? Email { get; set; }
        public Role Role { get; set; }
    }
}
