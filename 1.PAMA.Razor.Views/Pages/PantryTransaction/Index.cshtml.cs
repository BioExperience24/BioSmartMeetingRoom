using Microsoft.AspNetCore.Mvc.RazorPages;

namespace _1.PAMA.Razor.Views.Pages.PantryTransaction;

public class IndexModel(IConfiguration config) : PageModel
{        //ADDSON
    public string? BaseUrl { get; private set; } = config["ApiUrls:BaseUrl"];
    public string? BaseWeb { get; private set; } = config["App:BaseUrl"];

    //CRUD
    public string? GetAll { get; private set; } = config["ApiUrls:Endpoints:PantryTransaksi:GetAll"];
    public string? Delete { get; private set; } = config["ApiUrls:Endpoints:PantryTransaksi:Delete"];
    public string? GetById { get; private set; } = config["ApiUrls:Endpoints:PantryTransaksi:GetById"];
    public string? Create { get; private set; } = config["ApiUrls:Endpoints:PantryTransaksi:Create"];
    public string? Update { get; private set; } = config["ApiUrls:Endpoints:PantryTransaksi:Update"];

    //pantry
    public string? GetAllPantry { get; private set; } = config["ApiUrls:Endpoints:Pantry:GetAll"];

}
