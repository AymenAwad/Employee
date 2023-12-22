using System;
using System.Globalization;

namespace Shared.Exceptions
{
    public class ForbiddenAccessException : Exception
    {

        public ForbiddenAccessException() : base() { }

        public ForbiddenAccessException(string message) : base(message) { }

        public ForbiddenAccessException(string message, params object[] args)
            : base(string.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}
