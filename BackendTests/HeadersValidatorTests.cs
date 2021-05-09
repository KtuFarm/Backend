using Backend.Services.HeadersValidator;
using Backend.Services.RequestValidator;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using NUnit.Framework;
using static NUnit.Framework.Assert;

namespace BackendTests
{
    public class HeadersValidatorTests
    {
        private IHeadersValidator _headersValidator;

        [SetUp]
        public void SetUp()
        {
            _headersValidator = new HeadersValidator();
        }

        [Test]
        public void TestEmptyHeader()
        {
            var headers = new HeaderDictionary();

            IsFalse(IsValid(headers));
        }

        [Test]
        public void TestValidHeader()
        {
            var headers = new HeaderDictionary
            {
                new("X-Api-Request", "true")
            };

            IsTrue(IsValid(headers));
        }

        [Test]
        public void TestInvalidHeaderValue()
        {
            var headers = new HeaderDictionary
            {
                new("X-Api-Request", "not_true")
            };

            IsFalse(IsValid(headers));
        }

        [Test]
        public void TestMultipleHeaderValues()
        {
            var values = new StringValues(
                new[]
                {
                    "true",
                    "false"
                }
            );

            var headers = new HeaderDictionary
            {
                new("X-Api-Request", values)
            };

            IsFalse(IsValid(headers));
        }

        [Test]
        public void TestInvalidHeaderKey()
        {
            var headers = new HeaderDictionary
            {
                new("Y-Api-Request", "true")
            };

            IsFalse(IsValid(headers));
        }

        private bool IsValid(IHeaderDictionary headers)
        {
            return _headersValidator.IsRequestHeaderValid(headers);
        }
    }
}
