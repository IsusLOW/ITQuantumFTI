using System.Net;

namespace Shared.Common.Exceptions
{
    public sealed class NotFoundException(string resourceName, object key) 
        : AppException($"{resourceName} with identifier '{key}' was not found.", HttpStatusCode.NotFound)
    {
    }
}