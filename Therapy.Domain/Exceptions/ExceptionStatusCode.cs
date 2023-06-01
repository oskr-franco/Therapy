using System.Net;
namespace Therapy.Domain.Exceptions
{
    public enum ExceptionStatusCode
    {
        BadRequest = HttpStatusCode.BadRequest,
        Unauthorized = HttpStatusCode.Unauthorized,
        Forbidden = HttpStatusCode.Forbidden,
        NotFound = HttpStatusCode.NotFound,
        InternalServerError = HttpStatusCode.InternalServerError,
    }
}