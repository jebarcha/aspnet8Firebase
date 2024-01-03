
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Netfirebase.Api.Models.Domain;
using Netfirebase.Api.Models.Enums;

namespace Netfirebase.Api.Models.Configurations;

public class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermission>
{
    public void Configure(EntityTypeBuilder<RolePermission> builder)
    {
        builder.HasKey(x => new { x.RoleId, x.PermissionId });
        builder.HasData(
            Create(Role.Client, PermissionEnum.ReadUser),
            Create(Role.Client, PermissionEnum.WriteUser)
        );
    }

    private static RolePermission Create(Role role, PermissionEnum permission)
    {
        return new RolePermission
        {
            RoleId = role.Id,
            PermissionId = (int)permission
        };
    }

}
