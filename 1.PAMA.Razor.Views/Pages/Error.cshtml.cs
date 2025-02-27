using _5.Helpers.Consumer._Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;
using System.Net;

namespace PAMA1.Pages
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    [IgnoreAntiforgeryToken]
    public class ErrorModel(IConfiguration config) : PageModel
    {
        public string AppUrl { get; set; } = config["App:BaseUrl"] ?? string.Empty;
        public string? RequestId { get; set; }
        public new int StatusCode { get; set; }
        public string StatusMessage { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;

        public void OnGet(int code)
        {
            HttpStatusCode httpStatusCode = (HttpStatusCode)code;

            StatusCode = code;
            StatusMessage = _Dictionary.StatusCodeMessage.ContainsKey(code) ? _Dictionary.StatusCodeMessage[code] : _Dictionary.StatusCodeMessage[404];
            Title = httpStatusCode.ToString();

            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
        }
    }

}
