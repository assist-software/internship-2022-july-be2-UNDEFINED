using Assist.July._2022.BE2.Application.Dtos.UserDtos;
using Assist.July._2022.BE2.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Assist.July._2022.BE2.Application.Interfaces
{
    public interface IUserService
    {
        Task<User> Login(LoginRequest Login);

        Task Register(string Email, string Password);

        Task<User> getUser(Guid Id);

        Task<User> getUserEmail(string Email);

        Task resetPassword(string Email);

        Task updateUser(UpdateRequest Update,Guid id);

        Task< IEnumerable<User>> getAll();

        Task deleteUser(Guid Id);
    }
}
