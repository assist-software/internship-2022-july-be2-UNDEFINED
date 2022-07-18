using Assist.July._2022.BE2.Application.Dtos.UserDtos;
using Assist.July._2022.BE2.Application.Interfaces;
using Assist.July._2022.BE2.Application.Helper;
using Assist.July._2022.BE2.Domain.Entities;
using Assist.July._2022.BE2.Infrastructure.Contexts;
using AutoMapper;

namespace Assist.July._2022.BE2.Application.Services
{
    public class UserService:IUserService
    {
        ApplicationDbContext applicationDbContext;
        readonly IMapper Mapper;
        public UserService(ApplicationDbContext DataBase,IMapper mapper)
        {
            applicationDbContext = DataBase;
            Mapper = mapper;
        }

        public User Login(LoginRequest Login)
        {
            var user = applicationDbContext.Users.SingleOrDefault(x => x.Email == Login.Email);
            if (user == null || user.Password != Login.Password)
                throw new AppException("Username or password is incorrect");

            return user;
        }
        public void Register(RegisterRequest Register)
        {
            if (applicationDbContext.Users.Any(x => x.Email == Register.Email))
                throw new AppException("Email " + Register.Email + " is already taken");
            var user = Mapper.Map<User>(Register);
            user.IsActive = true;
            user.CreatedAt = DateTime.Now;
            applicationDbContext.Users.AddAsync(user);
            applicationDbContext.SaveChanges();
        }
        public User GetUser(Guid Id)
        {
            var user = applicationDbContext.Users.Find(Id);
            if (user == null)
                throw new AppException("User not found");

            return user;
        }
        public void ResetPassword(string email)
        {
            var user = applicationDbContext.Users.Find(email);
            if (user == null)
                throw new AppException("Wrong Mail");
            user.Password = CreateNewPassword();
            applicationDbContext.Update(user);
            applicationDbContext.SaveChangesAsync();
        }
        public void UpdateUser(UpdateRequest Update,Guid id)
        {
            var user = GetUser(id);
            if (!string.IsNullOrEmpty(Update.Password))
                user.Password = Update.Password;
            Mapper.Map(Update, user);
            applicationDbContext.Users.Update(user);
            applicationDbContext.SaveChangesAsync();
        }
        public async Task<IEnumerable<User>>GetAll()
        { 
            return applicationDbContext.Users;
        }
        public void DeleteUser(Guid Id)
        {
            var user = GetUser(Id);
            if (user == null)
                throw new AppException("User not found");
            applicationDbContext.Users.Remove(user);
            applicationDbContext.SaveChangesAsync();
        }
        string CreateNewPassword()
        {
            string validChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*?_-";
            Random random = new Random();
            string Password=string.Empty;
            for (int i = 0; i < 8; i++)
            {
                Password+= validChars[random.Next(0, validChars.Length)];
            }

            return Password;
        }
    }
}
