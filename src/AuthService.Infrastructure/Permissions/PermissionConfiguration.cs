using AuthService.Domain.Permissions;
using AuthService.Domain.Roles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedKernel.Infrastructure.Database;

namespace AuthService.Infrastructure.Permissions;
internal sealed class PermissionConfiguration :IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {

        builder.ToTable(TableNames.Permissions);
        builder.HasKey(p => p.Id);

        var permissions = Enum.GetValues<SharedKernel.Infrastructure.Permissions.AuthService.Permission>()
            .Select(p => new Permission
            {
                Id = (int)p,
                Name = p.ToString()
            });

        builder.HasData(permissions);
    }
}
