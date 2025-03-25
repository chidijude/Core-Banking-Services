using AuthService.Application.Abstractions.Authentication;
using AuthService.Application.Abstractions.Data;
using AuthService.Application.Users.GetByEmail;
using AuthService.Domain.Users;
using Microsoft.EntityFrameworkCore;
using SharedKernel;
using SharedKernel.Application.Abstractions.Messaging;

namespace Application.Users.GetByEmail;

internal sealed class GetUserByEmailQueryHandler(IApplicationDbContext context, IUserContext userContext)
    : IQueryHandler<GetUserByEmailQuery, UserResponse>
{
    public async Task<Result<UserResponse>> Handle(GetUserByEmailQuery query, CancellationToken cancellationToken)
    {
        UserResponse? user = await context.Users
            .Where(u => u.Email == query.Email)
            .Select(u => new UserResponse
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email
            })
            .SingleOrDefaultAsync(cancellationToken);

        if (user is null)
        {
            return Result.Failure<UserResponse>(UserErrors.NotFoundByEmail);
        }

        if (user.Id != userContext.UserId)
        {
            return Result.Failure<UserResponse>(UserErrors.Unauthorized());
        }

        return user;
    }
}
