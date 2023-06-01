namespace Therapy.Domain.Exceptions
{
    public class ValidationException : TherapyException
    {
        public IEnumerable<string> Errors { get; }

        public ValidationException(IEnumerable<string> errors)
            : base("One or more validation errors occurred.")
        {
            this.Errors = errors;
            this.StatusCode = (int)ExceptionStatusCode.BadRequest;

        }

        public ValidationException(string error)
            : base("One or more validation errors occurred.")
        {
            this.Errors = new List<string> { error };
            this.StatusCode = (int)ExceptionStatusCode.BadRequest;

        }
    }
}