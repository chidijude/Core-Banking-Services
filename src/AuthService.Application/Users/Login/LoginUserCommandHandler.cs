﻿using SharedKernel.Application.Abstractions.Messaging;
using AuthService.Application.Abstractions.Authentication;
using AuthService.Application.Abstractions.Data;
using AuthService.Domain.Users;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace AuthService.Application.Users.Login;

internal sealed class LoginUserCommandHandler(
    IApplicationDbContext context,
    IPasswordHasher passwordHasher,
    ITokenProvider tokenProvider) : ICommandHandler<LoginUserCommand, string>
{
    public async Task<Result<string>> Handle(LoginUserCommand command, CancellationToken cancellationToken)
    {
        User? user = await context.Users
            .AsNoTracking()
            .SingleOrDefaultAsync(u => u.Email == command.Email, cancellationToken);

        if (user is null)
        {
            return Result.Failure<string>(UserErrors.NotFoundByEmail);
        }

        bool verified = passwordHasher.Verify(command.Password, user.PasswordHash);

        if (!verified)
        {
            return Result.Failure<string>(UserErrors.NotFoundByEmail);
        }

        string token = await tokenProvider.CreateAsync(user);

        return token;
    }
}
