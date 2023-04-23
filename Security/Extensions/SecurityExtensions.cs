using Metika.Security.Middleware;
using Microsoft.AspNetCore.Builder;

namespace Metika.Security.Extensions
{
    public static class SecurityExtensions
    {
        public static IApplicationBuilder UseSecurityFeatures(this IApplicationBuilder app)
        {
            app.UseMiddleware<ResponseSecurityCheckMiddleware>();
            return app;
        }
    }
}
