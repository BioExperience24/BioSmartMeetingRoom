using System.Text.Json;
using _4.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace _1.PAMA.Razor.Views.Pages.Setting.Preview;

public class IndexModel : PageModel
{
    [BindProperty]
    public SettingSmtpVMPreview Data { get; set; } = new SettingSmtpVMPreview{};

    public IActionResult OnGet()
    {
        if (!IsAjaxRequest())  
        {  
            return NotFound();
        }
        return Page(); 
    }

    public IActionResult OnPost()  
    {
        if (!IsAjaxRequest())  
        {  
            return NotFound();
        }
        return Page();  
    }

    private bool IsAjaxRequest()  
    {  
        return Request.Headers["X-Requested-With"] == "XMLHttpRequest";  
    } 
}

