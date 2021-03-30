﻿using System.Linq;
using Backend.Models;
using Backend.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

namespace Backend.Controllers
{
    public class ApiControllerBase : ControllerBase
    {
        protected const string ApiContentType = "application/json";

        protected ApiContext Context { get; }

        private const string ApiHeader = "X-Api-Request";

        public ApiControllerBase(ApiContext context)
        {
            Context = context;
        }

        protected bool IsValidApiRequest()
        {
            Request.Headers.TryGetValue(ApiHeader, out var headers);
            return IsHeaderLegit(headers) && headers == "true";
        }

        private static bool IsHeaderLegit(StringValues headers)
        {
            return !(headers.Count != 1 || string.IsNullOrWhiteSpace(headers.FirstOrDefault()));
        }

        protected BadRequestObjectResult ApiBadRequest(string message)
        {
            var error = new ErrorDTO
            {
                Type = 400,
                Title = message
            };
            return BadRequest(error);
        }
    }
}
