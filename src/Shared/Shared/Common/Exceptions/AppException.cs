using System.Net;

namespace Shared.Common.Exceptions
{
    public abstract class AppException
        (string message, 
        HttpStatusCode statusCode = HttpStatusCode.InternalServerError) 
        : Exception(message)
    {
        public HttpStatusCode StatusCode { get; } = statusCode;
    }
}