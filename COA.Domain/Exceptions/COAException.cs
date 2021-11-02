using System;

namespace COA.Domain.Exceptions
{
    public class COAException : Exception
    {
        public COAException(string message) : base(message) { }
    }
}
