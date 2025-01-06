using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace _1.PAMA.Razor.Views.Pages.Pantry;


[Authorize]
public class PantryPackageModel(IConfiguration config) : PageModel
{
    //CRUD
    public string? GetAll { get; private set; } = config["ApiUrls:Endpoints:PantryPackage:GetAll"];
    public string? Delete { get; private set; } = config["ApiUrls:Endpoints:PantryPackage:Delete"];
    public string? GetById { get; private set; } = config["ApiUrls:Endpoints:PantryPackage:GetById"];
    public string? Create { get; private set; } = config["ApiUrls:Endpoints:PantryPackage:Create"];
    public string? Update { get; private set; } = config["ApiUrls:Endpoints:PantryPackage:Update"];

    //CRUDSATUAN
    //CRUD
    public string? GetAllSatuan { get; private set; } = config["ApiUrls:Endpoints:PantrySatuan:GetAll"];
    public string? DeleteSatuan { get; private set; } = config["ApiUrls:Endpoints:PantrySatuan:Delete"];
    public string? GetByIdSatuan { get; private set; } = config["ApiUrls:Endpoints:PantrySatuan:GetById"];
    public string? CreateSatuan { get; private set; } = config["ApiUrls:Endpoints:PantrySatuan:Create"];
    public string? UpdateSatuan { get; private set; } = config["ApiUrls:Endpoints:PantrySatuan:Update"];

    //ADDSON
    public string? BaseUrl { get; private set; } = config["ApiUrls:BaseUrl"];
    public string? BaseWeb { get; private set; } = config["App:BaseUrl"];

    public string? GetAllPantryAndImage { get; private set; } = config["ApiUrls:Endpoints:Pantry:GetAll"];
    //pantry detail/MENU
    public string? GetByPantryId { get; private set; } = config["ApiUrls:Endpoints:PantryDetail:GetByPantryId"];


}
