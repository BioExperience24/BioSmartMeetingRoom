using System.Text.Json;
using _4.Data.ViewModels;
using Microsoft.AspNetCore.Mvc.Filters;

namespace _1.PAMA.Razor.Views.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public class PermissionAccessAttribute : Attribute, IPageFilter
    {
        private readonly string? _page;

        public PermissionAccessAttribute()
        {}

        public PermissionAccessAttribute(string? page)
        {
            _page = page;
        }

        public void OnPageHandlerSelected(PageHandlerSelectedContext context)
        {
            // Console.WriteLine("-----------OnPageHandlerSelected-----------");
            // var requestPath = _page ?? context.HttpContext.Request.Path.ToString();
            // Console.WriteLine("requestPath: " + requestPath);
        }

        public void OnPageHandlerExecuting(PageHandlerExecutingContext context)
        {
            var requestPath = _page ?? context.HttpContext.Request.Path.ToString();

            if (context.HttpContext.Request.Cookies.TryGetValue("AuthInfoId", out var authInfoId))
            {
                var authInfoSessionKey = $"AuthInfo-{authInfoId}";
                var jsonAuthInfo = context.HttpContext.Session.GetString(authInfoSessionKey);
                if (!string.IsNullOrEmpty(jsonAuthInfo))
                {
                    var authInfo = JsonSerializer.Deserialize<UserViewModel>(jsonAuthInfo);
                    List<MenuVM> menus = authInfo?.SideMenu ?? new List<MenuVM>();
                    if (menus.Any())
                    {
                        if (!IsPathExists(menus, requestPath))
                        {
                            context.HttpContext.Response.Redirect("/error/403");
                        }
                    }
                }
            }
        }

        public void OnPageHandlerExecuted(PageHandlerExecutedContext context)
        {
            // Console.WriteLine("-----------OnPageHandlerExecuted-----------");
            // var requestPath = _page ?? context.HttpContext.Request.Path.ToString();
            // Console.WriteLine("requestPath: " + requestPath);
        }

        private bool IsPathExists(List<MenuVM> menus, string path)
        {
            return menus.Any(menu => menu.MenuUrl == path || (menu.Child.Any() && IsPathExists(menu.Child, path)));
        }
    }
}