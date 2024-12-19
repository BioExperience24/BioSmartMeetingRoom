using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _5.Helpers.Consumer._Response
{
    public class _Json
    {

        private readonly HttpContext? _httpContext;

        private HttpStatusCode _statusCode = HttpStatusCode.OK;

        private string? _status;

        private string? _message;

        private object? _collection;

        public _Json(HttpContext httpContext)
        {
            _httpContext = httpContext;
        }

        public _Json SetHttpCode(HttpStatusCode httpStatusCode)
        {
            _statusCode = httpStatusCode;
            return this;
        }

        public _Json SetHeader(string key, string value)
        {
            _httpContext!.Response.Headers[key] = value;
            
            return this;
        }

        public _Json Set(string? status, string? message, object? collection)
        {
            _status = status;
            _message = message;
            _collection = collection;
            return this;
        }

        public _Json SetStatus(string status)
        {
            _status = status;
            return this;
        }

        public _Json SetMessage(string message)
        {
            _message = message;
            return this;
        }
        

        public _Json SetCollection(object collection)
        {
            _collection = collection;
            return this;
        }

        public ActionResult Generate()
        {
            var data = new _EntityJson{};

            if (_status != null)
            {
                data.Status = _status;
            }

            if (_message != null)
            {
                data.Message = _message;
            }

            if (_collection != null)
            {
                data.Collection = _collection;
            }
            

            if (_statusCode == HttpStatusCode.NoContent)
            {
                return new _JsonResult(_statusCode);
            }
            
            return new _JsonResult(_statusCode, JsonSerializer.Serialize(data));
        }
    }
}