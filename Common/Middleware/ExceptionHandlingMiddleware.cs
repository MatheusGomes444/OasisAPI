using System.Net;
using System.Text.Json;
using OasisApi.Common.Exceptions;

namespace OasisApi.Common.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
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
                var statusCode = ex switch
                {
                    NotFoundException => HttpStatusCode.NotFound,
                    BadRequestException => HttpStatusCode.BadRequest,
                    UnauthorizedException => HttpStatusCode.Unauthorized,
                    _ => HttpStatusCode.InternalServerError
                };

                if (statusCode == HttpStatusCode.InternalServerError)
                {
                    _logger.LogError(ex, "Erro não tratado ao processar {Path}", context.Request.Path);
                }

                var message = statusCode == HttpStatusCode.InternalServerError
                    ? "Ocorreu um erro interno ao processar a solicitação."
                    : ex.Message;

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)statusCode;
                await context.Response.WriteAsync(JsonSerializer.Serialize(new { message }));
            }
        }
    }
}
