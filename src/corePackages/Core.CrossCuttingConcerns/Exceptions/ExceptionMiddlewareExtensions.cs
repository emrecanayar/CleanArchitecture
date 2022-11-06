using Microsoft.AspNetCore.Builder;

namespace Core.CrossCuttingConcerns.Exceptions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void UseConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }

    }
}
