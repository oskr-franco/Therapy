namespace Therapy.Domain.Exceptions
{
    public class InternalServerErrorException : TherapyException
    {
        public InternalServerErrorException(string message)
            : base(message)
        {
            this.StatusCode = (int)ExceptionStatusCode.InternalServerError;
        }
    }
}