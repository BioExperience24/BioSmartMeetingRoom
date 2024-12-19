
using Microsoft.AspNetCore.Mvc;

namespace _1.PAMA.Razor.Views.Components
{
    [ViewComponent(Name = "Navbar")]
    public class NavbarViewComponent(IConfiguration config) : ViewComponent
    {
        private readonly IConfiguration _config = config;

        public IViewComponentResult Invoke(string pageName)
        {
            var model = new {
                PageName = pageName,
                AppUrl = _config["App:BaseUrl"]
            };
            
            return View("Default", model);
        }
    }
}