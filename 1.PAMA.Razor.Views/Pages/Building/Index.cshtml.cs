using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _1.PAMA.Razor.Views.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace _1.PAMA.Razor.Views.Pages.Building
{
    [Authorize]
    [RejectWebviewUser]
    [PermissionAccess]
    public class IndexModel : PageModel
    {
        private readonly IConfiguration _config;

        public IndexModel(IConfiguration config)
        {
            _config = config;
        }

        public string BaseUrl { get; private set; } = string.Empty;
        public string GetBuildings { get; private set; } = string.Empty;
        public string CreateBuilding { get; private set; } = string.Empty;
        public string GetBuilding { get; private set; } = string.Empty;
        public string UpdateBuilding { get; private set; } = string.Empty;
        public string DeleteBuilding { get; private set; } = string.Empty;
        // public string ContentBuilding { get; private set;} = string.Empty;

        public void OnGet()
        {
            var baseUrl = _config["ApiUrls:BaseUrl"];
            BaseUrl = $"{baseUrl}";

            GetBuildings = $"{_config["ApiUrls:Endpoints:GetBuildings"]}";
            CreateBuilding = $"{_config["ApiUrls:Endpoints:CreateBuilding"]}";
            GetBuilding = $"{_config["ApiUrls:Endpoints:GetBuilding"]}";
            UpdateBuilding = $"{_config["ApiUrls:Endpoints:UpdateBuilding"]}";
            DeleteBuilding = $"{_config["ApiUrls:Endpoints:DeleteBuilding"]}";

            // Menggabungkan path ke file content.cshtml di folder Pages/Building
            // var contentFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Pages", "Building", "content.cshtml");
            // ContentBuilding = System.IO.File.ReadAllText(contentFilePath); // Membaca isi file sebagai string
        }
    }
}