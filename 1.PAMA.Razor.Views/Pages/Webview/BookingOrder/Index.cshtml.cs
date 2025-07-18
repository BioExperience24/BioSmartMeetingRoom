using Microsoft.AspNetCore.Mvc.RazorPages;

namespace _1.PAMA.Razor.Views.Pages.Webview.BookingOrder;

public class IndexModel(IConfiguration config) : PageModel
{
    public string AppUrl { get; set; } = config["App:BaseUrl"] ?? string.Empty;
    public string ApiUrl { get; set; } = config["ApiUrls:BaseApi"] ?? string.Empty;
    public string GetClaims { get; set; } = config["ApiUrls:Endpoints:Webview:User:GetClaims"] 
        ?? string.Empty;
    public string GetDataTables { get; set; } = config["ApiUrls:Endpoints:Webview:Booking:GetDataTables"] 
        ?? string.Empty;
    public string CancelBooking { get; set; } = config["ApiUrls:Endpoints:Webview:Booking:CancelBooking"] 
        ?? string.Empty;
        
    public string CancelAllBooking { get; set; } = config["ApiUrls:Endpoints:Webview:Booking:CancelAllBooking"] 
        ?? string.Empty;

    public string EndMeeting { get; set; } = config["ApiUrls:Endpoints:Webview:Booking:EndMeeting"]
        ?? string.Empty;
    public string CheckAvailableTimeBooking { get; set; } = config["ApiUrls:Endpoints:Webview:Booking:CheckAvailableTimeBooking"] 
        ?? string.Empty;
    public string RescheduleBooking { get; set; } = config["ApiUrls:Endpoints:Webview:Booking:RescheduleBooking"] 
        ?? string.Empty;

    public void OnGet()
    {
    }
}
