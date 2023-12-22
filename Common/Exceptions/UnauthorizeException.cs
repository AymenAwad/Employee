using Localization.Resources;
using System;


namespace Shared.Exceptions
{
    public class UnauthorizeException : Exception
    {
        public UnauthorizeException() : base(SharedResource.UnauthorizeMessage)
        {

        }
    }
}
