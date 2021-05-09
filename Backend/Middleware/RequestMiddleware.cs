using Backend.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System.Linq;
using System.Threading.Tasks;
using Backend.Models.Common;
using Newtonsoft.Json;

namespace Backend.Middleware
{
    public class RequestMiddleware
    {
        private const string ApiHeader = "X-Api-Request";

        private readonly RequestDelegate _next;

        private string SerializedError
        {
            get
            {
                var error = new ErrorDTO
                {
                    Type = 400,
                    Title = ApiErrorSlug.InvalidHeaders,
                    Details = null
                };

                return JsonConvert.SerializeObject(error, Formatting.Indented);
            }
        }

        public RequestMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            if (httpContext.Request.Path.StartsWithSegments("/api"))
            {
                httpContext.Request.Headers.TryGetValue(ApiHeader, out var headers);

                if (!IsValidRequest(headers))
                {
                    httpContext.Response.StatusCode = 400;

                    await httpContext.Response.WriteAsync(SerializedError);
                    return;
                }
            }

            await _next(httpContext);
        }

        private static bool IsValidRequest(StringValues headers)
        {
            return IsHeaderLegit(headers) && headers == "true";
        }

        private static bool IsHeaderLegit(StringValues headers)
        {
            return !(headers.Count != 1 || string.IsNullOrWhiteSpace(headers.FirstOrDefault()));
        }
    }
}
