using Assist.July._2022.BE2.Domain.Entities;
using Assist.July._2022.BE2.Infrastructure.Contexts;
using Assist.July._2022.BE2.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Assist.July._2022.BE2.Infrastructure.Repositories
{
    public class UserRepository:IUserRepository
    {
        private readonly ApplicationDbContext applicationDbContext;

        public UserRepository(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await applicationDbContext.Users.ToListAsync();
        }

        public async Task AddAsync(User user)
        {
            applicationDbContext.Users.Add(user);

            await applicationDbContext.SaveChangesAsync();
        }

        public async Task PutAsync(User UpdatedUser)
        {
            await applicationDbContext.SaveChangesAsync();
        }

        public async Task<User> GetByIdAsync(Guid id)
        {
            var user = await applicationDbContext.Users.FindAsync(id);

            if (user == null)
            {
                return null;
            }

            return user;
        }
        public async Task<User>GetByEmaiAsync(string Email)
        {
            var user=await applicationDbContext.Users
                .SingleOrDefaultAsync(x=> x.Email == Email);
            if (user == null)
                return null;
            return user;
        }
        public async Task DeleteAsync(User user)
        {
            applicationDbContext.Users.Remove(user);

            await applicationDbContext.SaveChangesAsync();
        }

    }
}
