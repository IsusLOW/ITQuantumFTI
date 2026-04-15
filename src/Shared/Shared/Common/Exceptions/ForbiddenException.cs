using System.Net;

namespace Shared.Common.Exceptions
{
    public sealed class ForbiddenException(string message)
        : AppException(message, HttpStatusCode.Forbidden)
    {
    }
}
