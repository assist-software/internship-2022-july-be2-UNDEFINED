using Assist.July._2022.BE2.Application.Dtos.UserDtos;
using Assist.July._2022.BE2.Application.Interfaces;
using Assist.July._2022.BE2.Application.Helper;
using Assist.July._2022.BE2.Domain.Entities;
using Assist.July._2022.BE2.Infrastructure.Contexts;
using AutoMapper;
using Assist.July._2022.BE2.Application.Dtos.MailDtos;
using Assist.July._2022.BE2.Infrastructure.Interfaces;

namespace Assist.July._2022.BE2.Application.Services
{
    public class UserService:IUserService
    {
        IMailService MailService;
        ApplicationDbContext applicationDbContext;
        readonly IMapper Mapper;
        private IJwtUtils JwtUtil;
        private IUserRepository UserRepository;
        public UserService(ApplicationDbContext DataBase,IMapper mapper, IMailService mailService
            , IJwtUtils jwtUtil,IUserRepository UserRepo)
        {
            applicationDbContext = DataBase;
            Mapper = mapper;
            MailService = mailService;
            JwtUtil = jwtUtil;
            UserRepository = UserRepo;
        }

        public async Task<User> Login(LoginRequest Login)
        {
            var user = await UserRepository.GetByEmaiAsync(Login.Email);
            if (user == null || user.Password != Login.Password)
                throw new AppException("Username or password is incorrect");
            user.Token = JwtUtil.GenerateToken(user);
            await UserRepository.PutAsync(user);
            return user;
        }
        public async Task Register(RegisterRequest Register)
        {
            var email = await UserRepository.GetByEmaiAsync(Register.Email);
            if (email!=null)
                throw new AppException("Email " + Register.Email + " is already taken");
            
            var user = Mapper.Map<User>(Register);
            user.IsActive = true;
            user.CreatedAt = DateTime.Now;
            user.UpdatedAt = DateTime.Now;
            if (user.Password == String.Empty)
                throw new AppException("Password Required");
            await UserRepository.AddAsync(user);
            await UserRepository.PutAsync(user);
        }
        public async Task<User> GetUser(Guid Id)
        {
            var user = await UserRepository.GetByIdAsync(Id);
            if (user == null)
                throw new AppException("User not found");

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
            user.Password = CreateNewPassword();
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
            Mapper.Map(Update, user);
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
