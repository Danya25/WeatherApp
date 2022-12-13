using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;
using WeatherApp.Domain.Exceptions;
using WeatherApp.Domain.Models;

namespace WeatherApp.Middleware
{
    public class ApiExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ApiExceptionHandlingMiddleware> _logger;

        public ApiExceptionHandlingMiddleware(RequestDelegate next, ILogger<ApiExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ValidationException ex)
            {
                await HandleExceptionAsync(context, ex);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
                _logger.LogCritical(ex, "An error");
            }
        }
        private async Task HandleExceptionAsync(HttpContext context, Exception ex, int statusCode = (int)HttpStatusCode.BadRequest)
        {
            /*var endpointFeature = context.Features[typeof(IEndpointFeature)] as IEndpointFeature;
            var endpoint = endpointFeature?.Endpoint;
            var routePattern = (endpoint as RouteEndpoint)?.RoutePattern?.RawText;*/

            var problemDetails = ex.ToErrorMethodResult<object>();

            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";

            var result = JsonSerializer.Serialize(problemDetails);
            await context.Response.WriteAsync(result);
        }

    }
}
