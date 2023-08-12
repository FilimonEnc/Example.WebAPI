using Example.Application.Exceptions;
using Example.Infrastructure.Exceptions;

using FluentValidation;

using Microsoft.AspNetCore.Mvc;

using System.Net;
using System.Text.Json;

namespace Example.WebAPI.Middleware
{
    public class CustomExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomExceptionHandlerMiddleware(RequestDelegate next) => _next = next;

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                await HandleExceptionAsync(context, exception);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            var code = HttpStatusCode.InternalServerError;
            ProblemDetails problemDetails = new();

            switch (exception)
            {
                case ValidationException validationException:
                    code = HttpStatusCode.BadRequest;
                    foreach (var err in validationException.Errors)
                    {
                        problemDetails.Detail += err.ErrorMessage;
                    }
                    break;

                case NotFoundException:
                    code = HttpStatusCode.NotFound;
                    problemDetails.Detail = exception.Message;
                    break;

                case ForbiddenException:
                    code = HttpStatusCode.Forbidden;
                    problemDetails.Detail = exception.Message;
                    break;

                case InfrastructureException:
                    code = HttpStatusCode.InternalServerError;
                    problemDetails.Detail = exception.Message;
                    break;
                case BadRequestException:
                    code = HttpStatusCode.BadRequest;
                    problemDetails.Detail = exception.Message;
                    break;
            }

            context.Response.StatusCode = (int)code;
            problemDetails.Status = (int)code;
            problemDetails.Instance = context.Request.Path;
            if (string.IsNullOrEmpty(problemDetails.Detail))
            {
                problemDetails.Detail = exception.Message;
                if (exception.InnerException != null) problemDetails.Detail += "\n" + exception.InnerException.Message;
                problemDetails.Detail = problemDetails.Detail.Replace("\"", "");
                Console.Write(exception.StackTrace);
            }

            var result = JsonSerializer.Serialize(problemDetails);
            return context.Response.WriteAsync(result);
        }
    }
}
