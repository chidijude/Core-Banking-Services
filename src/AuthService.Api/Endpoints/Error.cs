using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;
using SharedKernel.Application.Abstractions.Exceptions;

namespace AuthService.Api.Endpoints;

internal sealed class Error : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("error", (HttpContext httpContext) =>
        {
            Exception? exception = httpContext.Features.Get<IExceptionHandlerFeature>()!.Error!;

            (int statusCode, string message) = exception switch
            {
                IServiceException serviceException => ((int)serviceException.StatusCode, serviceException.ErrorMessage),
                _ => (StatusCodes.Status500InternalServerError, "An unexpected error occurred.")
            };

            return Results.Problem(statusCode: statusCode, title: message);
        })
        .ExcludeFromDescription();        
    }
}
