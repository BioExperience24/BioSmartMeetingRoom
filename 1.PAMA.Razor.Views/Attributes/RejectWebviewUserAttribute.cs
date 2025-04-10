using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace _1.PAMA.Razor.Views.Attributes
{
    public class RejectWebviewUserAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;
            if (user?.Identity?.IsAuthenticated != true)
            {
                return;
            }

            var isWebview = user?.Claims.FirstOrDefault(c => c.Type == "IsWebview")?.Value;

            if (bool.TryParse(isWebview, out var isWebviewBool) && isWebviewBool)
            {
                var isAjax = context.HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest";

                if (isAjax || context.HttpContext.Request.Path.StartsWithSegments("/api"))
                {
                    context.Result = new ForbidResult();
                }
                else
                {
                    context.Result = new RedirectToPageResult("/Authentication");
                }
            }
        }
    }
}
