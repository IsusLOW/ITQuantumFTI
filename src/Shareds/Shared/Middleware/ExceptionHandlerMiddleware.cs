
using Microsoft.AspNetCore.Http;
using Shared.Common.Exceptions;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using FluentValidation;


namespace Shared.Middleware
{
    public class ExceptionHandlerMiddleware(RequestDelegate _next)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            if (exception is FluentValidation.ValidationException validationException)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;

                var errors = validationException.Errors
                    .GroupBy(e => e.PropertyName)
                    .ToDictionary(
                        g => g.Key,
                        g => g.Select(e => e.ErrorMessage).ToArray()
                    );

                await context.Response.WriteAsJsonAsync(new { errors });
            }
            if (exception is AppException appException)
            {
                context.Response.StatusCode = (int)appException.StatusCode;
                var response = new { error = appException.Message };
                var payload = JsonSerializer.Serialize(response);
                await context.Response.WriteAsync(payload);
            }
            else
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                var response = new { error = "Internal Server Error" };
                var payload = JsonSerializer.Serialize(response);
                await context.Response.WriteAsync(payload);
            }
        }
    }
}
