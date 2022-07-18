using Assist.July._2022.BE2.Application.Dtos.UserDtos;
using Assist.July._2022.BE2.Application.Helper;
using Assist.July._2022.BE2.Application.Interfaces;
using Assist.July._2022.BE2.Application.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Assist.Jully._2022.BE2.Controllers
{
    [Route("api/[controller]"), ApiController,AuthorizeAtribute]
    
    public class UserController : ControllerBase
    {
        private IUserService UserService;
        private IMapper Mapper;
        private readonly AppSettings AppSetting;
        public UserController(IUserService userService, IMapper mapper,
            IOptions<AppSettings>appsettings)
        {
            UserService = userService;
            Mapper = mapper;
            AppSetting = appsettings.Value;
        }

        [HttpPost("authenticate"),AllowAnonymus]
        public IActionResult Authenticate(LoginRequest Login)
        {
            try
            {
                var user=UserService.Login(Login);

                return new OkObjectResult(user);
            }
            catch(AppException ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }
        
        [HttpPost("Register"),AllowAnonymus]
        public IActionResult Register(RegisterRequest Register)
        {
            try
            {
                UserService.Register(Register);

                return new OkObjectResult("ok");
            }
            catch (AppException ex)
            {
                return new BadRequestObjectResult("An error has occured");
            }
        }
        
        [HttpPost("reset/password")]
        public IActionResult ResetPassword(string email)
        {
            try
            {
                UserService.ResetPassword(email);

                return new OkObjectResult("ok");
            }
            catch(AppException ex)
            {
                return new BadRequestObjectResult("An error has occured");
            }
        }
        
        [HttpPut("{id}")]
        public IActionResult Update(UpdateRequest Update,Guid id)
        {
            try
            {
                UserService.UpdateUser(Update,id);

                return new OkObjectResult("ok");
            }
            catch (AppException ex)
            {
                return new NotFoundObjectResult("User not found");
            }
        }
        
        [HttpGet("{id}")]
        public IActionResult GetUser(Guid id)
        {
            try
            {
                var user = UserService.GetUser(id);

                return new OkObjectResult(user);
            }
            catch(AppException ex)
            {
                return new NotFoundObjectResult("User not found");
            }
        }
        
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            { 
                return new OkObjectResult(UserService.GetAll());
            }
            catch(AppException ex)
            {
                return new NotFoundObjectResult("Database is empty");
            }
        }
        
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                UserService.DeleteUser(id);

                return new OkObjectResult("ok");
            }
            catch(AppException ex)
            {
                return new NotFoundObjectResult("User not found");
            }
        }
    }
}