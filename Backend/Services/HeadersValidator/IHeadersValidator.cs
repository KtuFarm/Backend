using Microsoft.AspNetCore.Http;

namespace Backend.Services.RequestValidator
{
    public interface IHeadersValidator
    {
        public bool IsRequestHeaderValid(IHeaderDictionary headers);
    }
}
