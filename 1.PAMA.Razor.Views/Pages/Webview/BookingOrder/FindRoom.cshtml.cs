using Microsoft.AspNetCore.Mvc.RazorPages;

namespace _1.PAMA.Razor.Views.Pages.Webview.BookingOrder
{
    public class FindRoomModel(IConfiguration config) : PageModel
    {
        public string AppUrl { get; set; } = config["App:BaseUrl"] ?? string.Empty;
        public string ApiUrl { get; set; } = config["ApiUrls:BaseApi"] ?? string.Empty;
        public string GetClaims { get; set; } = config["ApiUrls:Endpoints:Webview:User:GetClaims"]
                                                ?? string.Empty;
        public string GetBuildings { get; set; } = config["ApiUrls:Endpoints:Webview:Building:GetAll"] ?? string.Empty;
        public string GetFacilities { get; set; } = config["ApiUrls:Endpoints:Webview:Facility:GetAll"] ?? string.Empty;
        public string GetPantryPackages { get; set; } = config["ApiUrls:Endpoints:Webview:PantryPackage:GetAll"] ?? string.Empty;
        public string GetEmployees { get; set; } = config["ApiUrls:Endpoints:Webview:Employee:GetAll"] ?? string.Empty;
        public string GetAlocations { get; set; } = config["ApiUrls:Endpoints:Webview:Alocation:GetAll"] ?? string.Empty;
        public string GetAvailableRooms { get; set; } = config["ApiUrls:Endpoints:Webview:Room:GetAvailableRooms"] ?? string.Empty;

        public string GetPantryPackageById { get; set; } = config["ApiUrls:Endpoints:Webview:PantryPackage:GetById"] ?? string.Empty;

        public string CreateBooking { get; set; } = config["ApiUrls:Endpoints:Webview:Booking:CreateBooking"] 
        ?? string.Empty;
        
        public string Token { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public void OnGet(string token, string type)
        {
            Token = token;
            Type = type;

            if (Type != "room" && Type != "trainingroom" && Type != "specialroom" && Type != "noroom")
            {
                Response.Redirect($"/webview/booking/index?token={Token}");
            }
        }
    }
}