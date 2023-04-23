using Microsoft.AspNetCore.Http;

namespace Metika.Security.Middleware
{
    public class ResponseSecurityCheckMiddleware
    {
        private readonly RequestDelegate _next;
        public ResponseSecurityCheckMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
            context.Response.Headers.Add("Cache-Control", "no-cache, no-store, must-revalidate");
            context.Response.Headers.Add("Referrer-Policy", "no-referrer");
            //context.Response.Headers.Add("X-Permitted-Cross-Domain-Policies", "none");
            //context.Response.Headers.Add("Permissions-Policy", "accelerometer=(), camera=(), geolocation=(), gyroscope=(), magnetometer=(), microphone=(), payment=(), usb=()");
            await this._next(context);
        }
    }
}
