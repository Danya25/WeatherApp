using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;
using WeatherApp.Domain.Exceptions;
using WeatherApp.Domain.Extensions;
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
            catch(ParsingException ex)
            {
                await HandleParsingException(context, ex);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
                _logger.LogCritical(ex, "An error");
            }
        }

        private Task HandleParsingException(HttpContext context, ParsingException ex)
        {

            var problemDetails = ex.ToErrorMethodResult<object>();
            problemDetails.ExceptionMessage = problemDetails.ExceptionMessage.FirstCharToUpper();
            SetBedResponseWithJson(context);

            return WriteResponseIntoContext(problemDetails, context);
        }

        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var problemDetails = ex.ToErrorMethodResult<object>();
            SetBedResponseWithJson(context);
            return WriteResponseIntoContext(problemDetails, context);
        }



        private Task WriteResponseIntoContext(object data, HttpContext context)
        {
            var result = JsonSerializer.Serialize(data);
            return context.Response.WriteAsync(result);
        }
        private void SetBedResponseWithJson(HttpContext context)
        {
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.Response.ContentType = "application/json";
        }
    }
}
