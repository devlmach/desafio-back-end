using DesafioBackEnd.API.Domain.Errors;
using System.Net;
using System.Text.Json;

namespace DesafioBackEnd.API.Common.Middleware
{
    public class ExceptionsMiddeware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionsMiddeware> _logger;

        public ExceptionsMiddeware(RequestDelegate requestDelegate, ILogger<ExceptionsMiddeware> logger)
        {
            _next = requestDelegate;
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
                _logger.LogError(ex, ex.Message);
                context.Response.ContentType = "application/json";

                var statusCode = ex switch
                {
                    BadRequestException => (int)HttpStatusCode.BadRequest,
                    NotFoundException => (int)HttpStatusCode.NotFound,
                    DomainExceptions => (int)HttpStatusCode.BadRequest,
                };

                context.Response.StatusCode = statusCode;

                var response = new
                {
                    statusCode = statusCode,
                    message = ex.Message
                };

                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };

                await context.Response.WriteAsync(JsonSerializer.Serialize(response, options));
            }
        }
    }
}
