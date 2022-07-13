using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Assist.July._2022.BE2.Domain;
using Microsoft.AspNetCore.Authorization;

namespace Assist.Jully._2022.BE2.Controllers
{
    [Route("api/[controller]"), ApiController, Authorize]
    public class usercontroller : ControllerBase
    {
        
        [HttpPost("authenticate")]
        public IActionResult Authenticate(useraccess access)
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
        public IActionResult resetpassword()
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
        public IActionResult Getuser(int id)
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