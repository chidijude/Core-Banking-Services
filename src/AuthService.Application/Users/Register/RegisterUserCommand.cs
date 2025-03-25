using SharedKernel.Application.Abstractions.Messaging;

namespace AuthService.Application.Users.Register;

public sealed record RegisterUserCommand(string Email, string FirstName, string LastName, string Password)
    : ICommand<Guid>;
