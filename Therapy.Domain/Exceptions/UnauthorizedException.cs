namespace Therapy.Domain.Exceptions
{
    public class UnauthorizedException : TherapyException
    {
        public UnauthorizedException(int StatusCode)
            : base("Unauthorized access.")
        {
            this.StatusCode = (int)ExceptionStatusCode.Unauthorized;
        }
    }
}