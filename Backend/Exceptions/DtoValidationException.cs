using System;
using JetBrains.Annotations;

namespace Backend.Exceptions
{
    public class DtoValidationException : Exception
    {
        public string Parameter { get; }

        public DtoValidationException(string message, [CanBeNull] string parameter = null) : base(message)
        {
            Parameter = parameter ?? "";
        }
    }
}
