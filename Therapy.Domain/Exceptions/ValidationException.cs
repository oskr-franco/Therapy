namespace Therapy.Domain.Exceptions
{
    public class ValidationException : TherapyException
    {
        public Dictionary<string, IEnumerable<string>> Errors { get; }

        public ValidationException(Dictionary<string, IEnumerable<string>> errors)
            : base("One or more validation errors occurred.")
        {
            this.Errors = errors;
            this.StatusCode = (int)ExceptionStatusCode.BadRequest;

        }

        public ValidationException(string field, string error)
            : base("One or more validation errors occurred.")
        {
            this.Errors = new Dictionary<string, IEnumerable<string>> { { field, new List<string> { error } } };
            this.StatusCode = (int)ExceptionStatusCode.BadRequest;
        }
    }
}