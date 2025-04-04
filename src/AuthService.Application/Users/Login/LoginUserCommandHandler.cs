using SharedKernel.Application.Abstractions.Messaging;
using AuthService.Application.Abstractions.Authentication;
using AuthService.Application.Abstractions.Data;
using AuthService.Domain.Users;
using Microsoft.EntityFrameworkCore;
using SharedKernel;
using System.Linq;

namespace AuthService.Application.Users.Login;

internal sealed class LoginUserCommandHandler(
#pragma warning disable CS9113 // Parameter is unread.
    IApplicationDbContext context,
#pragma warning restore CS9113 // Parameter is unread.
    IPasswordHasher passwordHasher,
    ITokenProvider tokenProvider) : ICommandHandler<LoginUserCommand, string>
{
    public async Task<Result<string>> Handle(LoginUserCommand command, CancellationToken cancellationToken)
    {
        //User? user = await context.Users
        //    .AsNoTracking()
        //    .SingleOrDefaultAsync(u => u.Email == command.Email, cancellationToken);

        await Task.Delay(200, cancellationToken);
        List<User> users = TemporaryDatabase.Users;
        User? user = users
            .SingleOrDefault(u => u.Email == command.Email);

        if (user is null)
        {
            return Result.Failure<string>(UserErrors.NotFoundByEmail);
        }

        bool verified = passwordHasher.Verify(command.Password, user.PasswordHash);

        if (!verified)
        {
            return Result.Failure<string>(UserErrors.NotFoundByEmail);
        }

        string token = tokenProvider.Create(user);

        return token;
    }
}
