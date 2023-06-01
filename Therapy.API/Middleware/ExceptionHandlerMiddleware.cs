using System.Net;
using Therapy.Domain.Exceptions;
using System.Text.Json;

namespace Therapy.API.Middleware {
    /// <summary>
    ///  Middleware that catches exceptions thrown by the application and converts them to JSON.
    /// </summary>
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionHandlerMiddleware"/> class.
        /// </summary>
        /// <param name="next">The next middleware in the pipeline.</param>
        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        /// <summary>
        ///  Invokes the next middleware in the pipeline and catches any exceptions that occur.
        /// </summary>
        /// <param name="context">The current HTTP context.</param>
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";
            ErrorResponse errorResponse;
            if(ex is ValidationException validationException)
            {
                context.Response.StatusCode = validationException.StatusCode;
                errorResponse = new ErrorResponse(validationException);
            }
            else if(ex is TherapyException therapyException)
            {
                context.Response.StatusCode = therapyException.StatusCode;
                errorResponse = new ErrorResponse(therapyException);
            }
            else
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                errorResponse = new ErrorResponse(ex);
            }

            return context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));
        }
    }
}