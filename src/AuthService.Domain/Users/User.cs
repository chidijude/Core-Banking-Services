using AuthService.Domain.Permissions;
using AuthService.Domain.Roles;
using SharedKernel;

namespace AuthService.Domain.Users;

public sealed class User : Entity
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PasswordHash { get; set; }
    public ICollection<Role> Roles { get; set; }
}
