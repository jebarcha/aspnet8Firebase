using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Netfirebase.Api.Models.Domain;

namespace Netfirebase.Api.Models.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");

        builder.HasKey(x => x.Id);

        builder.HasMany(x => x.Roles)
            .WithMany()
            .UsingEntity<UserRole>();
    }
}
