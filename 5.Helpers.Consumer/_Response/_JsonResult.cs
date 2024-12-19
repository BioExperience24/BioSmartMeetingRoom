using System.Net;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace _5.Helpers.Consumer._Response
{
    public class _JsonResult : ActionResult
    {
        private static readonly UTF8Encoding utf = new UTF8Encoding();

        public HttpStatusCode StatusCode { get; set; }

        public object? Data { get; set; }

        public Dictionary<string, string> Headers { get; private set; }

        public _JsonResult()
        {
            StatusCode = HttpStatusCode.OK;
            Headers = new Dictionary<string, string>();
        }

        public _JsonResult(HttpStatusCode statusCode)
            : this()
        {
            StatusCode = statusCode;
        }

        public _JsonResult(HttpStatusCode statusCode, object data)
            : this()
        {
            StatusCode = statusCode;
            Data = data;
        }

        private string Json
        {
            get
            {
                if (Data != null)
                {
                    return Data is string ? Data.ToString() ?? string.Empty : JsonSerializer.Serialize(Data);
                }
                return string.Empty;
            }
        }

        public byte[] GetBuffer() => utf.GetBytes(Json);

        public override void ExecuteResult(ActionContext context)
        {
            SetHeaders(context);
            SetResponse(context);
            context.HttpContext.Response.Body.Write(GetBuffer(), 0, GetBuffer().Length);
        }

        public override async Task ExecuteResultAsync(ActionContext context)
        {
            SetHeaders(context);
            SetResponse(context);
            await context.HttpContext.Response.Body.WriteAsync(GetBuffer(), 0, GetBuffer().Length);
        }

        private void SetHeaders(ActionContext context)
        {
            if (Headers.Count > 0)
            {
                foreach (var item in Headers)
                {
                    context.HttpContext.Response.Headers[item.Key] = item.Value;
                }
            }
        }

        private void SetResponse(ActionContext context)
        {
            context.HttpContext.Response.ContentType = "application/json; charset=utf-8";
            context.HttpContext.Response.StatusCode = (int)StatusCode;
        }
    }
}