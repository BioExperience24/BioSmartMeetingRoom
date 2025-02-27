
namespace _1.PAMA.Razor.Views.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly HashSet<int> _handledStatusCodes = new() { 400, 403, 404, 500, 502, 503 };

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            await _next(context);

            int statusCode = context.Response.StatusCode;

            if (_handledStatusCodes.Contains(statusCode))
            {
                context.Response.Redirect($"/error/{statusCode}");
            }
        }
    }

    // Extension method untuk memudahkan pemanggilan di `Program.cs`
    public static class CustomErrorHandlingMiddlewareExtensions
    {
        public static IApplicationBuilder UseErrorHandling(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ErrorHandlingMiddleware>();
        }
    }
}