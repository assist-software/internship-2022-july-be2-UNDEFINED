using Assist.July._2022.BE2.Domain.Entities;

namespace Assist.July._2022.BE2.Infrastructure.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task AddAsync(User user);
        Task PutAsync(User UpdatedUser);
        Task<User> GetByIdAsync(Guid id);
        Task<User> GetByEmaiAsync(string Email);
        Task DeleteAsync(User user);
    }
}
