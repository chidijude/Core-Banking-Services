using AuthService.Domain.Users;

namespace AuthService.Application.Abstractions.Authentication;

public interface ITokenProvider
{
    Task<string> CreateAsync(User user);
}
