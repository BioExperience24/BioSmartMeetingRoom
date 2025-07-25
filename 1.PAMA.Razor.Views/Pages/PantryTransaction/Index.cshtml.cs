using _1.PAMA.Razor.Views.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace _1.PAMA.Razor.Views.Pages.PantryTransaction;

[Authorize]
[PermissionAccess]
public class IndexModel(IConfiguration config) : PageModel
{        //ADDSON
    public string? BaseUrl { get; private set; } = config["ApiUrls:BaseUrl"];
    public string? BaseWeb { get; private set; } = config["App:BaseUrl"];

    //CRUD
    public string? GetAll { get; private set; } = config["ApiUrls:Endpoints:PantryTransaksi:GetAll"];
    //public string? Delete { get; private set; } = config["ApiUrls:Endpoints:PantryTransaksi:Delete"];
    //public string? GetById { get; private set; } = config["ApiUrls:Endpoints:PantryTransaksi:GetById"];
    //public string? Create { get; private set; } = config["ApiUrls:Endpoints:PantryTransaksi:Create"];
    //public string? Update { get; private set; } = config["ApiUrls:Endpoints:PantryTransaksi:Update"];

    //pantry
        public string? GetAllPantry { get; private set; } = config["ApiUrls:Endpoints:Pantry:GetAll"];
        public string? GetAllTransaksiStatus { get; private set; } = config["ApiUrls:Endpoints:PantryTransaksi:TransaksiStatus"];
        public string? ProcessCancelOrder { get; private set; } = config["ApiUrls:Endpoints:PantryTransaksi:ProcessCancelOrder"];

        public string PantryPackages { get; private set; } = 
                config["ApiUrls:Endpoints:PantryPackage:GetAll"] ?? string.Empty;
        public string PantryPackageDetailById { get; private set; } = 
                config["ApiUrls:Endpoints:PantryPackage:GetById"] ?? string.Empty;
        public string GetInProgressBookings { get; private set; } = 
                config["ApiUrls:Endpoints:Booking:GetInProgressBookings"] ?? string.Empty;
        public string CreateNewOrder { get; private set; } = 
                config["ApiUrls:Endpoints:Booking:CreateNewOrder"] ?? string.Empty;
        public string GetOrderDetail { get; private set; } = 
                config["ApiUrls:Endpoints:PantryTransaksi:GetPrintOrderApproval"] ?? string.Empty;
}
