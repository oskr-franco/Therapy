namespace Therapy.Domain.Exceptions
{
    public class TherapyException : Exception
    {
        public int StatusCode { get; set; }
        public TherapyException(string message) : base(message) { }
        public TherapyException(string message, Exception innerException) : base(message, innerException) { }
    }
}