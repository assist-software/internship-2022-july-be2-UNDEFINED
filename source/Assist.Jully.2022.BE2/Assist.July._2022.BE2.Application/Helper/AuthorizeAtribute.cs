using Assist.July._2022.BE2.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Assist.July._2022.BE2.Application.Helper
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAtribute:Attribute,IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var AllowAnonymus = context.ActionDescriptor.EndpointMetadata
                .OfType<AllowAnonymus>().Any();
            if (AllowAnonymus)
                return;
            var user = (User)context.HttpContext.Items["User"];
            if (user == null)
                context.Result = new JsonResult(new { Message = "Unauthorized" });
        }
    }
}
