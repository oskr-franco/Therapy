using System.Text.Json.Serialization;
using Therapy.Domain.Exceptions;

namespace Therapy.API.Middleware
{
    /// <summary>
    /// Middleware that catches exceptions thrown by the application and converts them to JSON.
    /// </summary>
    public class ErrorResponse
    {
        /// <summary>
        /// Message of the exception.
        /// </summary>
        [JsonPropertyName("title")]
        public string Title { get; set; }
        /// <summary>
        /// Errors that are specific to validation exceptions.
        /// </summary>
        [JsonPropertyName("errors")]
        public Dictionary<string, IEnumerable<string>>? Errors { get; set; }
        /// <summary>
        /// Status code of the exception.
        /// </summary>
        [JsonPropertyName("status")]
        public int StatusCode { get; set; }
        /// <summary>
        /// Converts the error response to a string.
        /// </summary>
        /// <param name="ex">The exception to convert.</param>
        public ErrorResponse(Exception ex)
        {
            this.Title = ex.Message;
            this.StatusCode = (int)ExceptionStatusCode.InternalServerError;
        }
        /// <summary>
        /// Converts the error response to a string.
        /// </summary>
        /// <param name="ex">The exception to convert.</param>
        public ErrorResponse(TherapyException ex)
        {
            this.Title = ex.Message;
            this.StatusCode = ex.StatusCode;
        }
        /// <summary>
        /// Converts the error response to a string.
        /// </summary>
        /// <param name="ex">The exception to convert.</param>
        public ErrorResponse(ValidationException ex)
        {
            this.Title = ex.Message;
            this.StatusCode = ex.StatusCode;
            this.Errors = ex.Errors;
        }
    }
}