using _1.PAMA.Razor.Views.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace _1.PAMA.Razor.Views.Pages.Pantry;


[Authorize]
[PermissionAccess]
public class IndexModel(IConfiguration config) : PageModel
{
    public string? BaseUrl { get; private set; } = config["ApiUrls:BaseUrl"];
    public string? BaseWeb { get; private set; } = config["App:BaseUrl"];
    public string? GetAllPantryAndImage { get; private set; } = config["ApiUrls:Endpoints:Pantry:GetAll"];
    public string? PantryImgView { get; private set; } = config["ApiUrls:Endpoints:Pantry:ImageView"];
    public string? Delete { get; private set; } = config["ApiUrls:Endpoints:Pantry:Delete"];
    public string? GetById { get; private set; } = config["ApiUrls:Endpoints:Pantry:GetById"];
    public string? Create { get; private set; } = config["ApiUrls:Endpoints:Pantry:Create"];
    public string? Update { get; private set; } = config["ApiUrls:Endpoints:Pantry:Update"];
    public string? GetAllSatuan { get; private set; } = config["ApiUrls:Endpoints:PantrySatuan:GetAll"];

    //pantry detail
    public string? GetByPantryId { get; private set; } = config["ApiUrls:Endpoints:PantryDetail:GetByPantryId"];
    public string? PDetailImageView { get; private set; } = config["ApiUrls:Endpoints:PantryDetail:ImageView"];
    public string? DeleteDetail { get; private set; } = config["ApiUrls:Endpoints:PantryDetail:Delete"];
    public string? GetByIdDetail { get; private set; } = config["ApiUrls:Endpoints:PantryDetail:GetById"];
    public string? CreateDetail { get; private set; } = config["ApiUrls:Endpoints:PantryDetail:Create"];
    public string? UpdateDetail { get; private set; } = config["ApiUrls:Endpoints:PantryDetail:Update"];

    //pantry variant
    public string? GetVariantByPDetailId { get; private set; } = config["ApiUrls:Endpoints:PantryVariant:GetByPDetailId"];
    public string? PVariantImageView { get; private set; } = config["ApiUrls:Endpoints:PantryVariant:ImageView"];
    public string? DeleteVariant { get; private set; } = config["ApiUrls:Endpoints:PantryVariant:Delete"];
    public string? GetByIdVariant { get; private set; } = config["ApiUrls:Endpoints:PantryVariant:GetById"];
    public string? CreateVariant { get; private set; } = config["ApiUrls:Endpoints:PantryVariant:Create"];
    public string? UpdateVariant { get; private set; } = config["ApiUrls:Endpoints:PantryVariant:Update"];

    public string AuthToken { get; set; }
    public void OnGet()
    {
        AuthToken = HttpContext.Request.Cookies["AuthToken"] ?? "INVALID-TOKEN";
    }
}
