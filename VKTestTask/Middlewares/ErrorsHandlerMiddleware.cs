using VKTestTask.Exceptions;

namespace VKTestTask.Middlewares;

public class ErrorsHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorsHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next.Invoke(context);
        }
        catch (ApiException e)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsync(e.Message);
        }
    }
}