using System.Collections.Generic;
using Backend.Exceptions;
using Backend.Models;
using Backend.Models.Common;
using JetBrains.Annotations;

namespace Backend.Services.Validators
{
    public class DTOValidator
    {
        [AssertionMethod]
        protected static void ValidateString(string parameter, string name)
        {
            if (string.IsNullOrEmpty(parameter))
                throw new DtoValidationException(ApiErrorSlug.EmptyParameter, name);
        }

        [AssertionMethod]
        protected static void ValidateNumberIsPositive(double number, string name)
        {
            if (number <= 0)
                throw new DtoValidationException(ApiErrorSlug.InvalidNumber, name);
        }

        [AssertionMethod]
        protected static void ValidateListNotEmpty<T>(List<T> list, string name)
        {
            if (list.Count == 0)
                throw new DtoValidationException(ApiErrorSlug.EmptyParameter, name);
        }
    }
}
