using Assist.July._2022.BE2.Application.Dtos.UserDtos;
using Assist.July._2022.BE2.Application.Helper;
using Assist.July._2022.BE2.Application.Interfaces;
using Assist.July._2022.BE2.Application.Services;
using Assist.July._2022.BE2.Infrastructure.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Assist.July._2022.BE2.Application.Dtos.InfoDtos;

namespace Assist.Jully._2022.BE2.Controllers
{
    [Route("api/[controller]"), ApiController]
    
    public class UserController : ControllerBase
    {
        private IUserService UserService;
        private IMapper Mapper;
        private readonly AppSettings AppSetting;
        private InfoMessegeDto message;
        public UserController(IUserService userService, IMapper mapper,
            IOptions<AppSettings>appsettings)
        {
            UserService = userService;
            Mapper = mapper;
            AppSetting = appsettings.Value;
            message = new InfoMessegeDto();
        }

        [HttpPost("Authenticate"),AllowAnonymus]
        public async Task<IActionResult> Authenticate(LoginRequest Login)
        {
            try
            {
                var user=await UserService.Login(Login);
                
                return new OkObjectResult(user);
            }
            catch(AppException ex)
            {
                return new BadRequestObjectResult(message.Error);
            }
        }
        
        [HttpPost("Register"),AllowAnonymus]
        public async Task<IActionResult> Register(string email,string password)
        {
            try
            {
                await UserService.Register(email,password);

                return new OkObjectResult(message.Ok);
            }
            catch (AppException ex)
            {
                return new BadRequestObjectResult(message.Take);
            }
        }
        
        [HttpPost("Reset/Password"),AllowAnonymus]
        public async Task<IActionResult> ResetPassword(string email)
        {
            try
            {
                await UserService.ResetPassword(email);

                return new OkObjectResult(message.Ok);
            }
            catch(AppException ex)
            {
                return new BadRequestObjectResult(message.ErrorEmail);
            }
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(UpdateRequest Update,Guid id)
        {
            try
            {
                await UserService.UpdateUser(Update,id);

                return new OkObjectResult(message.Ok);
            }
            catch (AppException ex)
            {
                return new NotFoundObjectResult(message.Error);
            }
        }
      
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(Guid id)
        {
            try
            {
                var user = await UserService.GetUser(id);

                return new OkObjectResult(user);
            }
            catch(AppException ex)
            {
                return new NotFoundObjectResult(message.Error);
            }
        }
        [HttpGet("search/{Email}")]
        public async Task<IActionResult> GetUserEmail(string Email)
        {
            try
            {
                var user = await UserService.GetUserEmail(Email);

                return new OkObjectResult(user);
            }
            catch(AppException ex)
            {
                return new NotFoundObjectResult(message.Error);
            }
        }
        
        [HttpGet,OnlyAdmins]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var users = await UserService.GetAll();
                return new OkObjectResult(users);
            }
            catch(AppException ex)
            {
                return new NotFoundObjectResult(message.ErrorEmpty);
            }
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await UserService.DeleteUser(id);

                return new OkObjectResult(message.Ok);
            }
            catch(AppException ex)
            {
                return new NotFoundObjectResult(message.Error);
            }
        }
 
    }
}