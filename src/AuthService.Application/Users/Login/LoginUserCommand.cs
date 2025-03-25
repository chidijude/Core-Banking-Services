using SharedKernel.Application.Abstractions.Messaging;

namespace AuthService.Application.Users.Login;

public sealed record LoginUserCommand(string Email, string Password) : ICommand<string>;
