namespace Therapy.Domain.Exceptions
{
    public class ForbiddenException : TherapyException
    {
        public ForbiddenException()
            : base("Access to the requested resource is forbidden.")
        {
            this.StatusCode = (int)ExceptionStatusCode.Forbidden;
        }
    }
}