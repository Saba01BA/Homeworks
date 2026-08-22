using System.Net;

namespace LoanApi.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
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
            catch (InvalidOperationException exception)
            {
                _logger.LogWarning(exception, "Request failed");
                await WriteLogToFile(exception);
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                await context.Response.WriteAsJsonAsync(new { message = exception.Message });
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Unexpected error");
                await WriteLogToFile(exception);
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await context.Response.WriteAsJsonAsync(new { message = "An unexpected error occurred." });
            }
        }

        private async Task WriteLogToFile(Exception exception)
        {
            var logsFolder = Path.Combine(AppContext.BaseDirectory, "Logs");
            Directory.CreateDirectory(logsFolder);
            var text = $"{DateTime.UtcNow}: {exception.Message}{Environment.NewLine}";
            await File.AppendAllTextAsync(Path.Combine(logsFolder, "errors.txt"), text);
        }
    }
}
