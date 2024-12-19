using Microsoft.AspNetCore.Mvc;

namespace _1.PAMA.Razor.Views.Components
{
    [ViewComponent(Name = "Sidebar")]
    public class SidebarViewComponent(IConfiguration config) : ViewComponent
    {

        private readonly IConfiguration _config = config;

        public IViewComponentResult Invoke()
        {
            var model = new {
                AppUrl = _config["App:BaseUrl"]
            };

            return View("Default", model);
        }
    }
}