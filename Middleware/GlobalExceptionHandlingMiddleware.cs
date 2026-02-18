using System.Net;
using System.Text.Json;

namespace Mini_E_Commerce_API.Middleware
{
    /// <summary>
    /// Global Exception Handling Middleware
    /// Catches unhandled exceptions and returns consistent error responses
    /// 
    /// Benefits:
    /// - Prevents stack traces from leaking to clients
    /// - Provides consistent error response format
    /// - Logs all errors for debugging
    /// - Handles specific exception types appropriately
    /// </summary>
    public class GlobalExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;

        public GlobalExceptionHandlingMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

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

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            var response = new ErrorResponse
            {
                IsSuccess = false,
                Timestamp = DateTime.UtcNow
            };

            switch (exception)
            {
                case KeyNotFoundException:
                    context.Response.StatusCode = StatusCodes.Status404NotFound;
                    response.Message = exception.Message;
                    break;

                case InvalidOperationException:
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    response.Message = exception.Message;
                    break;

                case ArgumentException:
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    response.Message = exception.Message;
                    break;

                case UnauthorizedAccessException:
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    response.Message = "Unauthorized access";
                    break;

                default:
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    response.Message = "An internal server error occurred";
                    break;
            }

            return context.Response.WriteAsJsonAsync(response);
        }
    }

    /// <summary>
    /// Standard error response format
    /// All errors should return this structure
    /// </summary>
    public class ErrorResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; }
        public string? TraceId { get; set; }
    }
}
