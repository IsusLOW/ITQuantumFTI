using System.Net;

namespace Shared.Common.Exceptions
{
    public sealed class NotFoundException : AppException
    {
        public NotFoundException(string resourceName, object key)
            : base($"{resourceName} with identifier '{key}' was not found.", HttpStatusCode.NotFound)
        {
        }
    }
}