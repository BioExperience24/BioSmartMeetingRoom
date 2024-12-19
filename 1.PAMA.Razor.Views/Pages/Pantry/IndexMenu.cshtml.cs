using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace _1.PAMA.Razor.Views.Pages.Pantry;

public class IndexMenuModel(IConfiguration config) : PageModel
{
    public string? BaseUrl { get; private set; } = config["ApiUrls:BaseUrl"];
    public string? BaseWeb { get; private set; } = config["App:BaseUrl"];

    public void OnGet()
    {
    }
}
