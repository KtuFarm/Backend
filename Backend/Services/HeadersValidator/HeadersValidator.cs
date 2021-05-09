using System.Linq;
using Backend.Services.RequestValidator;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace Backend.Services.HeadersValidator
{
    public class HeadersValidator : IHeadersValidator
    {
        private const string ApiHeader = "X-Api-Request";

        public bool IsRequestHeaderValid(IHeaderDictionary headers)
        {
            headers.TryGetValue(ApiHeader, out var headerValues);

            return IsValidRequest(headerValues);
        }

        private static bool IsValidRequest(StringValues headers)
        {
            return IsHeaderValid(headers) && headers == "true";
        }

        private static bool IsHeaderValid(StringValues headers)
        {
            return !(headers.Count != 1 || string.IsNullOrWhiteSpace(headers.FirstOrDefault()));
        }
    }
}
