using SharedKernel;

namespace AuthService.Domain.RelationshipMapping;
public class RolePermission : Entity
{
    public int RoleId { get; set; }
    public int PermissionId { get; set; }
}
