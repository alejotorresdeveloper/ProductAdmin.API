namespace ProductAdmin.API.Middleware;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using ProductAdmin.Domain.SharedKernel;
using System.Net;

public class ExceptionMiddleware
{
    private readonly ILogger<ExceptionMiddleware> _logger;
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next ?? throw new ArgumentNullException(nameof(next));
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        if (httpContext is null)
            return;

        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            if (httpContext.Response.HasStarted)
            {
                _logger.LogWarning(
                    "The response has already started, the http status code middleware will not be executed.");
                throw;
            }

            EventId eventId;
            switch (ex)
            {
                case BusinessException exception:
                    eventId = await httpContext.Response.ErrorResponseAsync(ex, exception.Code);
                    _logger.LogWarning(eventId, exception.Message);
                    break;

                case HttpRequestException exception:
                    eventId = await httpContext.Response.ErrorResponseAsync(ex, 401);
                    _logger.LogWarning(eventId, exception.Message);
                    break;

                default:
                    _logger.LogError(987, "Exception not controlled or logged: " + ex.Message);
                    await httpContext.Response.ErrorResponseAsync(ex, 999);
                    break;
            }
        }
    }
}

internal static class ErrorResponseTemplate
{
    public static Task<EventId> ErrorResponseAsync(this HttpResponse response,
       Exception businessException, int code)
    {
        (HttpStatusCode httpStatusCode, EventId eventId) = GetResponseCode(businessException, code);

        CustomResponse<object> responseBody = CustomResponse<object>.BuildError(eventId.Id,
                                                             businessException.Message,
                                                             null);
        string message = JsonConvert.SerializeObject(responseBody, new JsonSerializerSettings
        {
            ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            },
            Formatting = Newtonsoft.Json.Formatting.Indented
        });

        response.Clear();
        response.StatusCode = (int)httpStatusCode;
        response.ContentType = "application/json";
        response.WriteAsync(message);

        return Task.FromResult(eventId);
    }

    private static (HttpStatusCode, EventId) GetResponseCode(Exception exception, int code)
    {
        return exception switch
        {
            //Business
            BusinessException => (HttpStatusCode.BadRequest, code),

            //Default
            _ => (HttpStatusCode.InternalServerError, code)
        };
    }
}