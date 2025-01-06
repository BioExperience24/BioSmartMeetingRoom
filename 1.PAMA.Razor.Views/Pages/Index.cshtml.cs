using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PAMA1.Pages
{
    public class IndexModel(IConfiguration config) : PageModel
    {
        public string AppUrl { get; set; } = config["App:BaseUrl"] ?? string.Empty;

        public IActionResult OnGet()
        {
            // return Redirect($"{AppUrl}company");
            return Redirect($"{AppUrl}dashboard");
        }
    }
}
