using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Shared.Globalization
{


    public class GlobalizationMiddleware
    {
        private readonly RequestDelegate next;

        public GlobalizationMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            // Do something with context near the beginning of request processing.
            CultureHelper.InitializeCultureFromCookie(context);

            await next.Invoke(context);

            // Clean up.
        }
    }
    public static class GlobalizationMiddlewareExtensions
    {
        public static IApplicationBuilder UseGlobalization(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<GlobalizationMiddleware>();
        }
    }
}