using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Netfirebase.Api.Models.Domain;
using Netfirebase.Api.Models.Enums;

namespace Netfirebase.Api.Models.Configurations;

public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.ToTable("Permissions");

        builder.HasKey(x => x.Id);

        IEnumerable<Permission> permissions = Enum.GetValues<PermissionEnum>()
            .Select(p => new Permission
            {
                Id = (int)p,
                Name = p.ToString()
            });
     
        builder.HasData(permissions);

    }
}
