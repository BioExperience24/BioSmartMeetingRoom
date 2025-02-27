using _3.BusinessLogic.Services.Interface;
using _4.Data.ViewModels;
using _5.Helpers.Consumer.EnumType;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;
using System.Runtime.ConstrainedExecution;
using System.Security.Claims;

namespace _1.PAMA.Razor.Views.Pages;

public class AuthenticationModel(IUserService userService) : PageModel
{
    public void OnGet()
    {
    }

    [BindProperty]
    public required string Username { get; set; }

    [BindProperty]
    public required string Password { get; set; }

    public async Task<JsonResult> OnPostLogin()
    {
        LoginModel payload = new() { Username = Username, Password = Password };
        var cekLogin = await userService.RequestToken(payload);

        if (cekLogin != null && cekLogin.Collection != null && cekLogin.StatusCode == 200)
        {
            var userVM = (UserViewModel)cekLogin.Collection;
            var claims = new List<Claim> {
                new (ClaimTypes.NameIdentifier, userVM.Id?.ToString() ?? "InvalidUser"),
                new (ClaimTypes.Name, userVM.Username),
                new (ClaimTypes.Role, userVM.Levels?.FirstOrDefault()?.LevelName ?? "User")
            };

            var claimsIdentity = new ClaimsIdentity(claims, "CookieAuth");
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            await HttpContext.SignInAsync("CookieAuth", claimsPrincipal);

            HttpContext.Response.Cookies.Append("AuthToken", userVM.Token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict
            });

        }

        return new JsonResult(cekLogin);
    }

}
