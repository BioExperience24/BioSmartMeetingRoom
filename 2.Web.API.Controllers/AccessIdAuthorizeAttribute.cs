using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace _2.Web.API.Controllers;

public class AccessIdAuthorizeAttribute(string requiredAccessId)
    : AuthorizeAttribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var accessIdClaim = context.HttpContext.User.FindFirst(ClaimTypes.Actor)?.Value;

        if (accessIdClaim == null || !accessIdClaim.Split('#').Contains(requiredAccessId))
        {
            context.Result = new ForbidResult(); // Tidak diizinkan
        }
    }
}