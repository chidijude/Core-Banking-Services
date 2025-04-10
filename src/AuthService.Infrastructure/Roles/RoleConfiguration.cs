using AuthService.Domain.RelationshipMapping;
using AuthService.Domain.Roles;
using AuthService.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedKernel.Infrastructure.Database;

namespace AuthService.Infrastructure.Roles;
internal class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable(TableNames.Roles);
        builder.HasKey(r => r.Id);

        builder.HasMany(r => r.Permissions)            
            .WithMany()
            .UsingEntity<RolePermission>();       

        builder.HasData(Role.GetValues());
    }
}
