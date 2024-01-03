using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Netfirebase.Api.Models.Domain;

namespace Netfirebase.Api.Models.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("Roles");

        builder.HasKey(x => x.Id);

        builder.HasMany(x => x.Permissions)
            .WithMany()
            .UsingEntity<RolePermission>();

        //builder.HasMany(x => x.Users)
        //    .WithMany();


        //builder.HasData(Role.CreateEnumerations());
        builder.HasData(Role.GetValues());
    }
}
