using System;

namespace Backend.Exceptions
{
    public class DuplicateObjectException : Exception
    {
        public int Id { get; }

        public DuplicateObjectException(string message, int id) : base(message)
        {
            Id = id;
        }
    }
}
