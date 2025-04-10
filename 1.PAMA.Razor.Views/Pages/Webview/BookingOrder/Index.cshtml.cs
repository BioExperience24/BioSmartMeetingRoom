using _1.PAMA.Razor.Views.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace _1.PAMA.Razor.Views.Pages.Webview.BookingOrder;

// [Authorize]
// [PermissionAccess]
public class IndexModel : PageModel
{
    private readonly IConfiguration _config;

    public IndexModel(IConfiguration config)
    {
        _config = config;
        AppUrl = _config["App:BaseUrl"] ?? string.Empty;
        ApiUrl = _config["ApiUrls:BaseUrl"] ?? string.Empty;
        GetDataTables = _config["ApiUrls:Endpoints:Webview:Booking:GetDataTables"] ?? string.Empty;

    }

    public string AppUrl { get; private set; }
    public string ApiUrl { get; private set; }
    public string GetDataTables { get; private set; }

    public async Task OnGetAsync()
    {
        // Call the service to get room details
    }

    public IActionResult OnGetDetailOrderContent()
    {
        return Partial("Webview/PantryOrder/Components/_DetailOrderContent");
    }
}
