﻿namespace AuthService.Application.Abstractions.Authentication;

public interface IUserContext
{
    Guid UserId { get; }
}
