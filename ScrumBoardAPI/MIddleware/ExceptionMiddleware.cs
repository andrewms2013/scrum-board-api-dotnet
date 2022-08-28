using System.Net;
using Newtonsoft.Json;
using ScrumBoardAPI.Exceptions;

namespace ScrumBoardAPI.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        this._next = next;
        this._logger = logger;
    }

    public async Task InvokeAsync(HttpContext context) {
        try
        {
            await _next(context);
        } catch (Exception ex)
        {
            _logger.LogError(ex, $"Something went wrong during processing of {context.Request.Path}");
            await HandleExceptionAsync(context, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        context.Response.ContentType = "application/json";
        var statusCode = HttpStatusCode.InternalServerError;
        var errorDetails = new ErrorDetails("Failure", ex.Message);

        switch (ex)
        {
            case NotFoundException notFoundException:
                statusCode = HttpStatusCode.NotFound;
                errorDetails.ErrorType = "Not Fould";
                break;
            default:
                break;
        }

        var response = JsonConvert.SerializeObject(errorDetails);
        context.Response.StatusCode = (int)statusCode;
        return context.Response.WriteAsync(response);
    }
}

internal class ErrorDetails
{
    public string ErrorType { get; set; }


    public string ErrorMessage { get; set; }

    public ErrorDetails(string errorType, string errorMessage)
    {
        ErrorType = errorType;
        ErrorMessage = errorMessage;
    }

}
