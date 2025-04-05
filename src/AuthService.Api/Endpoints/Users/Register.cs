using AuthService.Application.Users.Register;
using MediatR;
using SharedKernel;
using AuthService.Api.Extensions;
using AuthService.Api.Infrastructure;

namespace AuthService.Api.Endpoints.Users;

internal sealed class Register : IEndpoint
{
    public sealed record Request(string Email, string FirstName, string LastName, string Password);

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("users/register", async (Request request, ISender sender, CancellationToken cancellationToken) =>
        {
            var command = new RegisterUserCommand(
                request.Email,
                request.FirstName,
                request.LastName,
                request.Password);

            Result<Guid> result = await sender.Send(command, cancellationToken);

            //return result.Match(Results.Ok, CustomResults.Problem);
            return result.Match(CreatedAtRoute, CustomResults.Problem);
        })
        .WithTags(Tags.Users);
    }

    public static IResult CreatedAtRoute<TValue>(TValue userId)
     => Results.CreatedAtRoute<object>("GetByUserId", new { userId }, userId);
}
