using System.Net;
using _3.BusinessLogic.Services.Interface;
using _5.Helpers.Consumer._Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace _1.PAMA.Razor.Views.Pages.User
{
    public class IndexModel : PageModel
    {

        private readonly HttpContext _httpContext;

        private readonly IConfiguration _config;

        public IndexModel(IHttpContextAccessor httpContextAccessor, IConfiguration config)
        {
            _httpContext = httpContextAccessor.HttpContext ?? throw new("HttpContext is not available.");

            _config = config;
        }

        public string GetLevels { get; private set; } = string.Empty;
        public string UpdateLevel { get; private set; } = string.Empty;
        public string GetLevelDescByLevelId { get; private set; } = string.Empty;
        public string GetUsers { get; private set; } = string.Empty;
        public string GetEmployeesWithoutUser { get; private set; } = string.Empty;
        public string GetEmployeeById { get; private set; } = string.Empty;
        public string CreateUser { get; private set; } = string.Empty;
        public string GetUserById { get; private set; } = string.Empty;
        public string UpdateUser { get; private set; } = string.Empty;
        public string DeleteUser { get; private set; } = string.Empty;
        public string DisableUser { get; private set; } = string.Empty;

        public void OnGet()
        {
            var baseUrl = _config["ApiUrls:BaseUrl"];
            GetLevels = $"{baseUrl}{_config["ApiUrls:Endpoints:GetLevels"]}";
            UpdateLevel = $"{baseUrl}{_config["ApiUrls:Endpoints:UpdateLevel"]}";
            GetLevelDescByLevelId = $"{baseUrl}{_config["ApiUrls:Endpoints:GetLevelDescByLevelId"]}";
            GetUsers = $"{baseUrl}{_config["ApiUrls:Endpoints:GetUsers"]}";
            GetEmployeesWithoutUser = $"{baseUrl}{_config["ApiUrls:Endpoints:GetEmployeesWithoutUser"]}";
            GetEmployeeById = $"{baseUrl}{_config["ApiUrls:Endpoints:GetEmployeeById"]}";
            CreateUser = $"{baseUrl}{_config["ApiUrls:Endpoints:CreateUser"]}";
            GetUserById = $"{baseUrl}{_config["ApiUrls:Endpoints:GetUserById"]}";
            UpdateUser = $"{baseUrl}{_config["ApiUrls:Endpoints:UpdateUser"]}";
            DeleteUser = $"{baseUrl}{_config["ApiUrls:Endpoints:DeleteUser"]}";
            DisableUser = $"{baseUrl}{_config["ApiUrls:Endpoints:DisableUser"]}";
        }
    }
}