using _3.BusinessLogic.Services.Interface;
using _4.Data.ViewModels;
using _5.Helpers.Consumer.EnumType;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;
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
        var cekLogin = await userService.CheckLogin(payload);

        if (cekLogin.StatusCode == 200)
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.Name, Username),
                new(ClaimTypes.Role, "admin")
            };

            var claimsIdentity = new ClaimsIdentity(claims, "CookieAuth");
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            await HttpContext.SignInAsync("CookieAuth", claimsPrincipal);
        }

        return new JsonResult(cekLogin);
    }

}
