using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace _1.PAMA.Razor.Views.Pages;

[Authorize]
public class LogoutModel : PageModel
{
    public async Task<IActionResult> OnGet()
    {
        await HttpContext.SignOutAsync("CookieAuth");

        // Hapus cookie authtoken
        HttpContext.Response.Cookies.Delete("AuthToken");

        if (Request.Cookies.TryGetValue("AuthInfoId", out var authInfoId))
        {
            HttpContext.Session.Remove($"AuthInfo-{authInfoId}");
            HttpContext.Response.Cookies.Delete("AuthInfoId");
        }

        return RedirectToPage("/Authentication");
    }
    public async Task<IActionResult> OnPostAsync()
    {
        // Logout user
        await HttpContext.SignOutAsync("CookieAuth");
        
        // Hapus cookie authtoken
        HttpContext.Response.Cookies.Delete("AuthToken");

        if (Request.Cookies.TryGetValue("AuthInfoId", out var authInfoId))
        {
            HttpContext.Session.Remove($"AuthInfo-{authInfoId}");
            HttpContext.Response.Cookies.Delete("AuthInfoId");
        }

        // Redirect ke halaman login atau homepage setelah logout
        return RedirectToPage("/Authentication");
    }
}
