using AuthService.Domain.Permissions;
using AuthService.Domain.Users;
using SharedKernel;

namespace AuthService.Domain.Roles;
public sealed class Role : Enumeration<Role> 
{
    public static readonly Role Admin = new(1, nameof(Admin));
    public Role(int id, string name) : base(id, name)
    {
    }

    public ICollection<Permission> Permissions { get; set; }
    public ICollection<User> Users { get; set; }
}
