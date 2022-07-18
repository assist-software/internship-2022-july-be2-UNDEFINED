using Assist.July._2022.BE2.Application.Dtos.UserDtos;
using Assist.July._2022.BE2.Domain.Entities;

namespace Assist.July._2022.BE2.Application.Interfaces
{
    public interface IUserService
    {
        public User Login(LoginRequest Login);
        public void Register(RegisterRequest Register);
        public User GetUser(Guid Id);
        public void ResetPassword(string email);
        public void UpdateUser(UpdateRequest Update,Guid id);
        public IEnumerable<User> GetAll();
        public void DeleteUser(Guid Id);
    }
}
