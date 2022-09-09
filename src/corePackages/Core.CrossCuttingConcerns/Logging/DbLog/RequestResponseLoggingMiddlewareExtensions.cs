using Core.CrossCuttingConcerns.Exceptions;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
