using _3.BusinessLogic.Services.Interface;
using _4.Data.ViewModels;
using _5.Helpers.Consumer.EnumType;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;
using System.Runtime.ConstrainedExecution;
using System.Security.Claims;
using System.Text.Json;

namespace _1.PAMA.Razor.Views.Pages.Webview;

public class AuthenticationModel(
    IUserService userService,
    IConfiguration config
) : PageModel
{
    public string AppUrl { get; set; } = config["App:BaseUrl"] ?? string.Empty;

    public IActionResult OnGet()
    {
        if (User.Identity != null && User.Identity.IsAuthenticated)
        {
            return Redirect($"{AppUrl}dashboard");
        }
        else if (Request.Cookies.TryGetValue("AuthCookie", out var authCookieValue))
        { // jika tidak cookie tidak valid tapi masih add cookienya

            HttpContext.Response.Cookies.Delete("AuthCookie");
            HttpContext.Response.Cookies.Delete("AuthToken");

            if (Request.Cookies.TryGetValue("AuthInfoId", out var authInfoId))
            {
                HttpContext.Session.Remove($"AuthInfo-{authInfoId}");
                HttpContext.Response.Cookies.Delete("AuthInfoId");
            }
        }

        return Page();
    }

    [BindProperty]
    public required string Username { get; set; }

    [BindProperty]
    public required string Nik { get; set; }

    [BindProperty]
    public string? ReturnUrl { get; set; }

    public async Task<JsonResult> OnPostLogin()
    {
        LoginWebviewModel payload = new() { Username = Username, Nik = Nik };
        var cekLogin = await userService.WebviewLogin(payload);

        if (cekLogin != null && cekLogin.Collection != null && cekLogin.StatusCode == 200)
        {
            var userVM = (UserViewModel)cekLogin.Collection;
            var claims = new List<Claim> {
                new (ClaimTypes.NameIdentifier, userVM.Id?.ToString() ?? "InvalidUser"),
                new (ClaimTypes.Name, userVM.Username),
                new (ClaimTypes.Role, userVM.LevelId.ToString() ?? "0"),
                new (ClaimTypes.UserData, Nik.ToString() ?? "InvalidNik"),
                new("IsWebview", "true")
                // new (ClaimTypes.Role, userVM.Levels?.FirstOrDefault()?.LevelName ?? "User")
            };

            var claimsIdentity = new ClaimsIdentity(claims, "CookieAuth");
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            await HttpContext.SignInAsync("CookieAuth", claimsPrincipal);

            bool isHttps = HttpContext.Request.IsHttps;

            HttpContext.Response.Cookies.Append("AuthToken", userVM.Token, new CookieOptions
            {
                HttpOnly = true,
                Secure = isHttps, // Hanya aktifkan Secure jika HTTPS
                SameSite = SameSiteMode.Strict
            });

            // Menyimpan JSON ke session
            string authInfoId = Guid.NewGuid().ToString();
            var authInfo = JsonSerializer.Serialize(userVM);

            HttpContext.Response.Cookies.Append("AuthInfoId", authInfoId, new CookieOptions
            {
                HttpOnly = true,
                Secure = isHttps, // Hanya aktifkan Secure jika HTTPS
                SameSite = SameSiteMode.Strict
            });
            HttpContext.Session.SetString($"AuthInfo-{authInfoId}", authInfo);

        }

        if (cekLogin!.Status == ReturnalType.Success)
        {
            cekLogin.Collection = (ReturnUrl != null) ? ReturnUrl : "webview/pantryorder/index";
        }

        return new JsonResult(cekLogin);
    }

}