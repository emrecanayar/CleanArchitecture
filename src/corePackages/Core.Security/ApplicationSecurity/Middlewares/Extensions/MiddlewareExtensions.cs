using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Core.Security.ApplicationSecurity.Middlewares.Extensions
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseIpSafe(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<IpSafeMiddleware>();
        }
    }
}
