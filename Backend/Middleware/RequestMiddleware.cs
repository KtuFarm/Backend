﻿using Backend.Models.DTO;
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
            if(httpContext.Request.Path.StartsWithSegments("/api"))
            {
                httpContext.Request.Headers.TryGetValue(ApiHeader, out var headers);
                if (!IsHeaderLegit(headers) || headers != "true")
                {
                    string json = GetErrorJson(httpContext);
                    await httpContext.Response.WriteAsync(json);
                    return;
                }
            }
            await _next(httpContext);
        }

        private static string GetErrorJson(HttpContext httpContext)
        {
            var error = new ErrorDTO
            {
                Type = 400,
                Title = "Invalid Headers!",
                Details = null
            };
            httpContext.Response.StatusCode = 400;
            return JsonConvert.SerializeObject(error, Formatting.Indented);
        }

        private static bool IsHeaderLegit(StringValues headers)
        {
            return !(headers.Count != 1 || string.IsNullOrWhiteSpace(headers.FirstOrDefault()));
        }
    }
}
