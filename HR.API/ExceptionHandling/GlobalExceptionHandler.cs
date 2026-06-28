using FluentValidation;
using HR.Application.Shared;
using Microsoft.AspNetCore.Diagnostics;
using System.ComponentModel.DataAnnotations;

namespace HR.API.ExceptionHandling
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            httpContext.Response.ContentType = "application/json";

            if(exception is FluentValidation.ValidationException validationException)
            {
                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;

                var errors =  validationException.Errors.Select(e => $"{e.PropertyName}: {e.ErrorMessage}").ToList();

                var response = ApiResponse<object>.FailureResponse(errors, "Validation failed");
                await httpContext.Response.WriteAsJsonAsync(response, cancellationToken);
                return true;
            }

            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            var serverErrorResponse = ApiResponse<object>.FailureResponse(
                new List<string> { exception.Message },
                "An unexpected internal error occurred"
                );

            await httpContext.Response.WriteAsJsonAsync(serverErrorResponse, cancellationToken);
            return true;
        }
    }
}
