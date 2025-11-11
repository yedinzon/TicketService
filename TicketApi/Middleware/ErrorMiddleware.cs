using System.Net;
using System.Text;
using System.Text.Json;

namespace TicketApi.Middleware;

/// <summary>
/// Handles errors globally and logs them.
/// </summary>
public class ErrorMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorMiddleware> _logger;

    public ErrorMiddleware(RequestDelegate next, ILogger<ErrorMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            context.Request.EnableBuffering();
            await _next(context);
        }
        catch (Exception ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            string requestBody = "";
            context.Request.Body.Position = 0;
            using (var reader = new StreamReader(context.Request.Body, Encoding.UTF8, detectEncodingFromByteOrderMarks: false, leaveOpen: true))
            {
                requestBody = await reader.ReadToEndAsync();
                context.Request.Body.Position = 0;
            }

            var logInfo = new
            {
                context.Request.Path,
                context.Request.Method,
                QueryString = context.Request.QueryString.ToString(),
                Body = requestBody,
                Exception = ex.ToString()
            };

            _logger.LogError(JsonSerializer.Serialize(logInfo));

            var response = new
            {
                context.Response.StatusCode,
                Message = "Ocurrió un error en el servidor."
            };

            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
