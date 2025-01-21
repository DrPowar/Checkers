namespace Checkers.Api.Middleware;

public class ExceptionHandlingMiddleware(RequestDelegate next, IHttpContextAccessor httpContextAccessor)
{
    private RequestDelegate _next = next ?? throw new ArgumentNullException(nameof(next));
    private IHttpContextAccessor _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleException(context, ex);
        }
    }
    
    private Task HandleException(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        
        var errorResponse = CreateErrorResponse(
            exception: exception,
            type: "https://example.com/problems/{error-type}",
            status: StatusCodes.Status500InternalServerError,
            title: "Internal Server Error",
            detail: "An unexpected error occurred."
        );
        
        switch (exception)
        {
            case KeyNotFoundException keyNotFoundException:
                return HandleKeyNotFoundException(context, keyNotFoundException);
            case ApplicationException applicationException:
                return HandleApplicationException(context, applicationException);
            default:
                return context.Response.WriteAsJsonAsync(errorResponse);
        }
    }
    
    private Task HandleKeyNotFoundException(HttpContext context, KeyNotFoundException exception)
    {
        var errorResponse = CreateErrorResponse(
            exception: exception,
            type: "https://example.com/problems/not-found-error",
            status: StatusCodes.Status404NotFound,
            title: "Resource Not Found",
            detail: exception.Message
        );

        context.Response.StatusCode = StatusCodes.Status404NotFound;
        context.Response.ContentType = "application/problem+json";
        
        return context.Response.WriteAsJsonAsync(errorResponse);
    }
    private Task HandleApplicationException(HttpContext context, ApplicationException exception)
    {
        var errorResponse = CreateErrorResponse(
            exception: exception.InnerException ?? exception,
            type: "https://example.com/problems/bad-request-error",
            status: StatusCodes.Status400BadRequest,
            title: "Bad Request",
            detail: exception.InnerException?.Message ?? exception.Message
        );

        context.Response.StatusCode = StatusCodes.Status400BadRequest;
        context.Response.ContentType = "application/problem+json";
        
        return context.Response.WriteAsJsonAsync(errorResponse);
    }
    private object CreateErrorResponse(Exception exception, string type, int status, string title, string detail)
    {
        //TODO: fix traceId
        return new
        {
            type = type,
            status = status,
            title = title,
            detail = detail,
            traceId = _httpContextAccessor.HttpContext?.TraceIdentifier ?? "N/A"
        };
    }
}