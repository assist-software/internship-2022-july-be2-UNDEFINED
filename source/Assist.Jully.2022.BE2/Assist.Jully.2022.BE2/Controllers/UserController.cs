using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Assist.July._2022.BE2.Domain.Entities;

namespace Assist.Jully._2022.BE2.Controllers
{
    [Route("api/[controller]"), ApiController, Authorize]
    
    public class UserController : ControllerBase
    {
        [HttpPost("authenticate")]
        public IActionResult Authenticate(User Access)
        {
            try
            {
                return new OkObjectResult("ok");
            }
            catch
            {
                return new BadRequestObjectResult("An error has occured");
            }
        }
        
        [HttpPost("Register")]
        public IActionResult Register()
        {
            try
            {
                return new OkObjectResult("ok");
            }
            catch
            {
                return new BadRequestObjectResult("An error has occured");
            }
        }
        
        [HttpPost("reset/password")]
        public IActionResult ResetPassword()
        {
            try
            {
                return new OkObjectResult("ok");
            }
            catch
            {
                return new BadRequestObjectResult("An error has occured");
            }
        }
        
        [HttpPut("{id}")]
        public IActionResult Update(int id)
        {
            try
            {
                return new OkObjectResult("ok");
            }
            catch
            {
                return new BadRequestObjectResult("An error has occured");
            }
        }
        
        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            try
            {
                return new OkObjectResult("ok");
            }
            catch
            {
                return new BadRequestObjectResult("An error has occured");
            }
        }
        
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                return new OkObjectResult("ok");
            }
            catch
            {
                return new BadRequestObjectResult("An error has occured");
            }
        }
        
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                return new OkObjectResult("ok");
            }
            catch
            {
                return new BadRequestObjectResult("An error has occured");
            }
        }
    }
}