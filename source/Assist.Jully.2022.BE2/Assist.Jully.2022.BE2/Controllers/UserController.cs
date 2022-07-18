using Assist.July._2022.BE2.Application.Dtos.UserDtos;
using Assist.July._2022.BE2.Application.Helper;
using Assist.July._2022.BE2.Application.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Assist.Jully._2022.BE2.Controllers
{
    [Route("api/[controller]"), ApiController]
    
    public class UserController : ControllerBase
    {
        private IUserService UserService;
        private IMapper Mapper;
        public UserController(IUserService userService, IMapper mapper)
        {
            UserService = userService;
            Mapper = mapper;
        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate(LoginRequest Login)
        {
            try
            {
                var user=UserService.Login(Login);

                return new OkObjectResult("ok");
            }
            catch(AppException ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }
        
        [HttpPost("Register")]
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