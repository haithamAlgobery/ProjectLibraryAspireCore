using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace BookApi.Middleware
{
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
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/problem+json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var problemDetails = new ProblemDetails
            {
                Title = "An unexpected error occurred!",
                Status = context.Response.StatusCode,
                Detail = exception.Message,
                Instance = context.Request.Path
            };

            var json = JsonSerializer.Serialize(problemDetails);

            return context.Response.WriteAsync(json);
        }
    }
}
