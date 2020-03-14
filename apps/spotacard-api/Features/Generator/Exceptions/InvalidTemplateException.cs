using System;

namespace Spotacard.Features.Generator.Exceptions
{
    public class InvalidTemplateException : Exception
    {
        public InvalidTemplateException(Guid templateId) : base($"{templateId} is not a valid template id")
        { 
        }
    }
}
