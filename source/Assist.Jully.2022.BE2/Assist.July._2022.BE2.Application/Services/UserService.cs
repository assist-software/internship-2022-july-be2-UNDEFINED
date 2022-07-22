using Assist.July._2022.BE2.Application.Dtos.UserDtos;
using Assist.July._2022.BE2.Application.Interfaces;
using Assist.July._2022.BE2.Application.Helper;
using Assist.July._2022.BE2.Domain.Entities;
using Assist.July._2022.BE2.Infrastructure.Contexts;
using AutoMapper;
using Assist.July._2022.BE2.Application.Dtos.MailDtos;
using Assist.July._2022.BE2.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;
using BCrypt.Net;
using Microsoft.Extensions.Options;

namespace Assist.July._2022.BE2.Application.Services
{
    public class UserService:IUserService
    {
        IMailService MailService;
        ApplicationDbContext applicationDbContext;
        readonly IMapper Mapper;
        private IJwtUtils JwtUtil;
        private IUserRepository UserRepository;
        private IAzureStorage Azure;
        readonly AzureSettings AzureSettings;
        public UserService(ApplicationDbContext DataBase,IMapper mapper, IMailService mailService
            , IJwtUtils jwtUtil, IUserRepository UserRepo, IAzureStorage azure,
            IOptions<AzureSettings>azureSettings)
        {
            applicationDbContext = DataBase;
            Mapper = mapper;
            MailService = mailService;
            JwtUtil = jwtUtil;
            UserRepository = UserRepo;
            Azure = azure;
            AzureSettings = azureSettings.Value;
        }

        public async Task<User> Login(LoginRequest Login)
        {
            var user = await UserRepository.GetByEmaiAsync(Login.Email);
            if (user.Google == false && user != null)
            {
                if (!BCrypt.Net.BCrypt.Verify(Login.Password, user.Password))
                    throw new AppException("password is incorrect");
            }
            else if (user == null)
                throw new AppException("username incorrect");
            else
            {
                user.Token = JwtUtil.GenerateToken(user);
                await UserRepository.PutAsync(user);
               
            }
            return user;
        }
        public async Task Register(string Email,string Password)
        {
            var email = await UserRepository.GetByEmaiAsync(Email);
            if (email != null)
                throw new AppException("Email " + Email + " is already taken");

            var user = new User();
            user.Email = Email;
            user.Password = BCrypt.Net.BCrypt.HashPassword(Password);
            user.Address = string.Empty;
            user.FullName = string.Empty;
            user.IsActive = true;
            user.Role = Domain.Enums.Role.User;
            user.Address = string.Empty;
            user.Phone = string.Empty;
            user.Address = string.Empty;
            user.CreatedAt = DateTime.Now;
            user.UpdatedAt = DateTime.Now;
            if (user.Password == String.Empty)
                throw new AppException("Password Required");
            await UserRepository.AddAsync(user);
            await UserRepository.PutAsync(user);
        }
        public async Task RegisterGoogle(GoogleDto Model)
        {
            var user = new User();
            Mapper.Map(Model, user);
            user.Role = Domain.Enums.Role.User;
            user.Password = BCrypt.Net.BCrypt.HashPassword(CreateNewPassword());
            user.CreatedAt = DateTime.Now;
            user.UpdatedAt = DateTime.Now;
            user.Google = true;
            user.Address = string.Empty;
            user.Phone = string.Empty;
            user.Address = string.Empty;
            await UserRepository.AddAsync(user);
            await UserRepository.PutAsync(user);
        }
        public async Task<User> GetUser(Guid Id)
        {
            var user = await UserRepository.GetByIdAsync(Id);
            if (user == null)
                throw new AppException("User not found");
            user.Photo +=AzureSettings.Key;
            return user;
        }
        public async Task<User> GetUserEmail(string Email)
        {
            var user = await UserRepository.GetByEmaiAsync(Email);
            if (user == null)
                throw new AppException("Mail not found");

            return user;
        }
        public async Task ResetPassword(string Email)
        {
            var user =await UserRepository.GetByEmaiAsync(Email);
            if (user == null)
                throw new AppException("Wrong Mail");
            user.Password = BCrypt.Net.BCrypt.HashPassword(CreateNewPassword());
            user.UpdatedAt = DateTime.Now;
            MailRequest MailToSend=new MailRequest();
            MailToSend.ToEmail = Email;
            MailToSend.Subject = "New Password";
            MailToSend.Body = user.Password;
            await UserRepository.PutAsync(user);
            await MailService.SendEmailAsync(MailToSend);
        }

        public async Task UpdateUser(UpdateRequest Update,Guid id)
        {
            var user = await UserRepository.GetByIdAsync(id);
            
            if (user == null)
                throw new AppException("User not found");
            Mapper.Map(Update, user);
            user.Photo = await Azure.UploadAsync64(Update.Photo, user.Id.ToString());
            if (!string.IsNullOrEmpty(Update.Password))
                user.Password = BCrypt.Net.BCrypt.HashPassword(Update.Password);
            await UserRepository.PutAsync(user);
           
        }
       
        public async Task<IEnumerable<User>>GetAll()
        {
            var user = await UserRepository.GetAllAsync();
            return user;
        }
        public async Task DeleteUser(Guid Id)
        {
            var user = await UserRepository.GetByIdAsync(Id);
            if (user == null)
                throw new AppException("User not found");
            await UserRepository.DeleteAsync(user);
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
