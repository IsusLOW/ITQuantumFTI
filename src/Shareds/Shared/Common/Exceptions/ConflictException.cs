using System.Net;

namespace Shared.Common.Exceptions
{
    public sealed class ConflictException(string message) 
        : AppException(message, HttpStatusCode.Conflict)
    {
    }
}