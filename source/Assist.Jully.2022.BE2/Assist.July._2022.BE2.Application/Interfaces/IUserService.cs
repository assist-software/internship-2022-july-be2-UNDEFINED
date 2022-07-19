using Assist.July._2022.BE2.Application.Dtos.UserDtos;
using Assist.July._2022.BE2.Domain.Entities;

namespace Assist.July._2022.BE2.Application.Interfaces
{
    public interface IUserService
    {
        Task<User> Login(LoginRequest Login);
        Task Register(string Email,string Password);
        Task<User> GetUser(Guid Id);
        Task<User> GetUserEmail(string Email);
        Task ResetPassword(string Email);
        Task UpdateUser(UpdateRequest Update,Guid id);
        Task< IEnumerable<User>> GetAll();
        Task DeleteUser(Guid Id);
    }
}
