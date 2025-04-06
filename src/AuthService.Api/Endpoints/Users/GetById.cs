using AuthService.Application.Users.GetById;
using MediatR;
using SharedKernel;
using AuthService.Api.Extensions;
using AuthService.Api.Infrastructure;

namespace AuthService.Api.Endpoints.Users;

internal sealed class GetById : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("users/{userId}", async (Guid userId, ISender sender, CancellationToken cancellationToken) =>
        {
            var query = new GetUserByIdQuery(userId);

            Result<UserResponse> result = await sender.Send(query, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Users)
        .WithName("GetByUserId")
        .RequireAuthorization()
        .RequireAuthorization(Permissions.UsersAccess);
    }
}
