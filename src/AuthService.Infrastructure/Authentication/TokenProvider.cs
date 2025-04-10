using System.Security.Claims;
using System.Text;
using AuthService.Application.Abstractions.Authentication;
using AuthService.Domain.Users;
using AuthService.Infrastructure.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace AuthService.Infrastructure.Authentication;

internal sealed class TokenProvider(IConfiguration configuration, PermissionProvider permissionProvider) : ITokenProvider
{
    public async Task<string> CreateAsync(User user)
    {
        string secretKey = configuration["Jwt:Secret"]!;
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(
            [
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email)
            ]),            
            Expires = DateTime.UtcNow.AddMinutes(configuration.GetValue<int>("Jwt:ExpirationInMinutes")),
            SigningCredentials = credentials,
            Issuer = configuration["Jwt:Issuer"],
            Audience = configuration["Jwt:Audience"]
        };

        // Uncomment this if you want to add permissions to the token
        var permissions = await permissionProvider.GetForUserIdAsync(user.Id);
        foreach (string permission in permissions)
        {
            tokenDescriptor.Subject.AddClaim(new Claim(CustomClaims.Permissions, permission));
        }

        var handler = new JsonWebTokenHandler();

        string token = handler.CreateToken(tokenDescriptor);

        return token;
    }
}
