using SharedKernel.Application.Abstractions.Messaging;
using AuthService.Application.Abstractions.Authentication;
using AuthService.Application.Abstractions.Data;
using AuthService.Domain.Users;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace AuthService.Application.Users.Register;

#pragma warning disable CS9113 // Parameter is unread.
internal sealed class RegisterUserCommandHandler(IApplicationDbContext context, IPasswordHasher passwordHasher)
#pragma warning restore CS9113 // Parameter is unread.
    : ICommandHandler<RegisterUserCommand, Guid>
{
    public async Task<Result<Guid>> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
    {
        //if (await context.Users.AnyAsync(u => u.Email == command.Email, cancellationToken))
        //{
        //    return Result.Failure<Guid>(UserErrors.EmailNotUnique);
        //}

        await Task.Delay(200, cancellationToken);
        bool b = TemporaryDatabase.Users.Any(u => u.Email == command.Email);
        if (b)
        {
            return Result.Failure<Guid>(UserErrors.EmailNotUnique);
        }

        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = command.Email,
            FirstName = command.FirstName,
            LastName = command.LastName,
            PasswordHash = passwordHasher.Hash(command.Password)
        };

        user.Raise(new UserRegisteredDomainEvent(user.Id));

        //context.Users.Add(user);

        //await context.SaveChangesAsync(cancellationToken);
        TemporaryDatabase.AddUser(user);

        return user.Id;
    }
}
