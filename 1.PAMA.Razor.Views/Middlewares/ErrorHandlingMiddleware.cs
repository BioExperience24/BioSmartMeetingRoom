
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
            try
            {
                await _next(context);

                // Tangani status error seperti 404 setelah pipeline selesai
                if (!context.Response.HasStarted && _handledStatusCodes.Contains(context.Response.StatusCode))
                {
                    context.Response.Redirect($"/error/{context.Response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                if (!context.Response.HasStarted)
                {
                    context.Response.Redirect("/Error");
                }
                else
                {
                    context.Response.ContentType = "text/plain";
                    await context.Response.WriteAsync("Terjadi kesalahan internal.");
                    Console.WriteLine("❗ Error: " + ex.Message);
                }
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