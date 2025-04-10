using _1.PAMA.Razor.Views.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace _1.PAMA.Razor.Views.Pages.Webview.PantryOrder;

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
        GetPantryTransactions = _config["ApiUrls:Endpoints:Webview:PantryTransaksi:GetAll"] ?? string.Empty;
        GetBookById = _config["ApiUrls:Endpoints:Webview:PantryTransaksi:GetPrintOrderApproval"] ?? string.Empty;
        GetPantryPackageById = _config["ApiUrls:Endpoints:Webview:PantryPackage:GetById"] ?? string.Empty;
        ProcessCancelOrder = _config["ApiUrls:Endpoints:Webview:PantryTransaksi:ProcessCancelOrder"] ?? string.Empty;
        GetInProgressBooking = _config["ApiUrls:Endpoints:Webview:Booking:GetInProgressBookings"] ?? string.Empty;
        GetAllPantryPackage = _config["ApiUrls:Endpoints:Webview:PantryPackage:GetAll"] ?? string.Empty;
        CreateOrderPackage = _config["ApiUrls:Endpoints:Webview:Booking:CreateNewOrder"] ?? string.Empty;

    }

    public string AppUrl { get; private set; }
    public string ApiUrl { get; private set; }
    public string GetPantryTransactions { get; private set; }
    public string GetBookById { get; private set; }
    public string GetPantryPackageById { get; private set; }
    public string ProcessCancelOrder { get; private set; }
    public string GetInProgressBooking { get; private set; }
    public string GetAllPantryPackage { get; private set; }
    public string CreateOrderPackage { get; private set; }

    public async Task OnGetAsync()
    {
        // Call the service to get room details
    }

    public IActionResult OnGetDetailOrderContent()
    {
        return Partial("Webview/PantryOrder/Components/_DetailOrderContent");
    }
}
