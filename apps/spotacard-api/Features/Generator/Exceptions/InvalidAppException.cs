using System;

namespace Spotacard.Features.Generator.Exceptions
{
    public class InvalidAppException : Exception
    {
        public InvalidAppException(Guid appId) : base($"{appId} is not a valid app id")
        {
        }
    }
}