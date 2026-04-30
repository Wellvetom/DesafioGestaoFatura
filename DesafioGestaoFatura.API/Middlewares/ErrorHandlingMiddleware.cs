using DesafioGestaoFatura.Domain.Exception;
using System.Net;
using System.Text.Json;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
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

    private static async Task HandleException(HttpContext context, Exception ex)
    {
        var statusCode = HttpStatusCode.InternalServerError;
        var message = "Erro interno";
        //Se foi pela camada do domain
        if (ex is DomainException)
        {
            statusCode = HttpStatusCode.BadRequest;
            message = ex.Message;
        }

        if (ex.Message.Contains("não encontrada"))
        {
            statusCode = HttpStatusCode.NotFound;
            message = ex.Message;
        }

        var response = new
        {
            message,
            statusCode = (int)statusCode
        };

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        var json = JsonSerializer.Serialize(response);

        await context.Response.WriteAsync(json);
    }
}