using AuthService.Domain.Users;

namespace AuthService.Application.Abstractions.Authentication;

public interface ITokenProvider
{
    string Create(User user);
}
