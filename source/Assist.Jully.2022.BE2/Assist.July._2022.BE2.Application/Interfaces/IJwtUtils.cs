using Assist.July._2022.BE2.Application.Dtos.UserDtos;
using Assist.July._2022.BE2.Domain.Entities;

namespace Assist.July._2022.BE2.Application.Interfaces
{
    public interface IJwtUtils
    {
        public string GenerateToken(User user);
        public UserToken? ValidateToken(string token);
    }
}
