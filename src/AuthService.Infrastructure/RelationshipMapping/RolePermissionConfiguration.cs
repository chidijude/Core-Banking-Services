using AuthService.Domain.RelationshipMapping;
using AuthService.Domain.Roles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedKernel.Infrastructure.Permissions.AuthService;

namespace AuthService.Infrastructure.RelationshipMapping;
internal sealed class RolePermissionConfiguration 
    : IEntityTypeConfiguration<RolePermission>
{
    public void Configure(EntityTypeBuilder<RolePermission> builder)
    {
        builder.HasKey(x => new { x.RoleId, x.PermissionId });

        builder.HasData(Create(Role.Admin, Permission.Create_User),
            Create(Role.Admin, Permission.Read_User));
    }

    private static RolePermission Create(Role role, Permission permission)
    {
        return new RolePermission
        {
            RoleId = role.Id,
            PermissionId = (int)permission
        };
    }
}
