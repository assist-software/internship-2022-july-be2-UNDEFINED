using Assist.July._2022.BE2.Application.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Assist.July._2022.BE2.Application.MiddleWare
{

    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context, IUserService userService, IJwtUtils jwtUtils)
        {

            var tok = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var userId = jwtUtils.ValidateToken(tok);
            if (userId != null)
            {
                context.Items["User"] = await userService.GetUser(userId.Value);
            }
            await _next(context);
        }
    }
}
