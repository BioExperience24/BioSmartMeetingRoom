using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PAMA1.Pages
{
    public class IndexModel(IConfiguration config) : PageModel
    {
        public string AppUrl { get; set; } = config["App:BaseUrl"] ?? string.Empty;

        public IActionResult OnGet()
        {

            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                return Redirect($"{AppUrl}dashboard");
            } else if (Request.Cookies.TryGetValue("AuthCookie", out var authCookieValue))  { // jika tidak cookie tidak valid tapi masih add cookienya
                
                HttpContext.Response.Cookies.Delete("AuthCookie");
                HttpContext.Response.Cookies.Delete("AuthToken");

                if (Request.Cookies.TryGetValue("AuthInfoId", out var authInfoId))
                {
                    HttpContext.Session.Remove($"AuthInfo-{authInfoId}");
                    HttpContext.Response.Cookies.Delete("AuthInfoId");
                }
            }
            
            return Redirect($"{AppUrl}Authentication");
        }
    }
}
