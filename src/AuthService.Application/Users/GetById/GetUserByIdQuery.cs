using SharedKernel.Application.Abstractions.Messaging;

namespace AuthService.Application.Users.GetById;

public sealed record GetUserByIdQuery(Guid UserId) : IQuery<UserResponse>;
