using _1.PAMA.Razor.Views.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace _1.PAMA.Razor.Views.Pages.Setting.SMTP;

[Authorize]
[PermissionAccess]
public class IndexModel : PageModel
{
    public void OnGet()
    {
    }
}
