using JetBrains.Annotations;
using System;

namespace Backend.Exceptions
{
    public class ResourceNotFoundException : Exception
    {
        public string Parameter { get; }

        public ResourceNotFoundException(string message, [CanBeNull] string parameter = null) : base(message)
        {
            Parameter = parameter ?? "";
        }
    }
}
