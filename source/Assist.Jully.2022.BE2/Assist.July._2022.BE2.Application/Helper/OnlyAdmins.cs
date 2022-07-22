using Assist.July._2022.BE2.Domain.Entities;
using Assist.July._2022.BE2.Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Assist.July._2022.BE2.Application.Helper
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class OnlyAdmins : Attribute, IAuthorizationFilter
    {
        public async void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = (User)context.HttpContext.Items["User"];
            Role AdminRole = Role.Admin;

            if(user.Role!=AdminRole)
               context.Result= new JsonResult(new { Message = "Unauthorized" });
        }
    }
}
