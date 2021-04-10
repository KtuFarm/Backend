using Backend.Exceptions;
using Backend.Models;
using JetBrains.Annotations;

namespace Backend.Services
{
    public class DTOValidator
    {
        [AssertionMethod]
        protected static void ValidateString(string parameter, string name)
        {
            if (string.IsNullOrEmpty(parameter))
                throw new DtoValidationException(ValidationError.EmptyParameter, name);
        }
    }
}
