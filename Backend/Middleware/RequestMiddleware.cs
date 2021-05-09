﻿using Backend.Models.DTO;
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
            if (httpContext.Request.Path.StartsWithSegments("/api"))
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
    }
}
