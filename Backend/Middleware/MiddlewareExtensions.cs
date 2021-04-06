using Microsoft.AspNetCore.Builder;

namespace Backend.Middleware
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestMiddleware>();
        }
    }
}
