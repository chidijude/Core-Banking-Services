﻿using SharedKernel;

namespace AuthService.Domain.Users;

public sealed record UserRegisteredDomainEvent(Guid UserId) : IDomainEvent;
