using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Backend.Middleware
{
    public class RequestMiddleware
    {
        private readonly RequestDelegate _next;
        public RequestMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext httpContext)
        {
            await _next(httpContext);
        }
    }
}
