using SharedKernel.Application.Abstractions.Messaging;
using Application.Users.GetByEmail;

namespace AuthService.Application.Users.GetByEmail;

public sealed record GetUserByEmailQuery(string Email) : IQuery<UserResponse>;
