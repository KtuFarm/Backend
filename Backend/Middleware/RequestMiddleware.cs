using Backend.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Backend.Middleware
{
    public class RequestMiddleware
    {
        private const string ApiHeader = "X-Api-Request";
        private readonly RequestDelegate _next;
        public RequestMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext httpContext)
        {
            httpContext.Request.Headers.TryGetValue(ApiHeader, out var headers);
            if (!IsHeaderLegit(headers) || headers != "true")
            {
                var error = new ErrorDTO
                {
                    Type = 400,
                    Title = "Invalid Headers!",
                    Details = null
                };
                httpContext.Response.StatusCode = 400;
                string jsonString = JsonConvert.SerializeObject(error, Formatting.Indented);
                await httpContext.Response.WriteAsync(jsonString);
                return;
            }
            await _next(httpContext);
        }
        private static bool IsHeaderLegit(StringValues headers)
        {
            return !(headers.Count != 1 || string.IsNullOrWhiteSpace(headers.FirstOrDefault()));
        }
    }
}
