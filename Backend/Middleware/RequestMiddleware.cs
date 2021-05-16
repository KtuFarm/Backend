using Backend.Models.DTO;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Backend.Models.Common;
using Backend.Services.RequestValidator;
using Newtonsoft.Json;

namespace Backend.Middleware
{
    public class RequestMiddleware
    {
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

        public async Task Invoke(HttpContext httpContext, IHeadersValidator validator)
        {
            if (IsPreflightRequest(httpContext))
            {
                httpContext.Response.StatusCode = 200;
                await _next(httpContext);
                return;
            }

            if (IsApiRequest(httpContext))
            {
                if (!validator.IsRequestHeaderValid(httpContext.Request.Headers))
                {
                    httpContext.Response.StatusCode = 400;

                    await httpContext.Response.WriteAsync(SerializedError);
                    return;
                }
            }

            await _next(httpContext);
        }

        private bool IsPreflightRequest(HttpContext httpContext) 
        {
            return httpContext.Request.Method == HttpMethods.Options;
        }

        private bool IsApiRequest(HttpContext httpContext) 
        {
            return httpContext.Request.Path.StartsWithSegments("/api");
        }
    }
}
