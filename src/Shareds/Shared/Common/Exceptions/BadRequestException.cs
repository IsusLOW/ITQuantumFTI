using System.Net;

namespace Shared.Common.Exceptions
{
    public sealed class BadRequestException(string message) 
        : AppException(message, HttpStatusCode.BadRequest)
    {
    }
}