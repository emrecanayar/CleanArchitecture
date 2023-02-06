using Microsoft.AspNetCore.Builder;

namespace Core.CrossCuttingConcerns.Logging.DbLog
{
    public static class RequestResponseLoggingMiddlewareExtensions
    {
        public static void UseRequestResponseLoggingMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<RequestResponseLoggingMiddleware>();
        }
    }
}