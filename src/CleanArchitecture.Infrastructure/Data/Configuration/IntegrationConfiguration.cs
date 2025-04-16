using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Data.Configuration;

public class IntegrationConfiguration : IEntityTypeConfiguration<Integration>
{
  public void Configure(EntityTypeBuilder<Integration> builder)
  {
    builder.HasKey(i => i.Id);

    builder.Property(i => i.ServiceName).IsRequired().HasMaxLength(50);
    builder.Property(i => i.AccessTokenEncrypted).IsRequired(); // BLOB/byte[]
                                                                // RefreshTokenEncrypted is nullable

    builder.Property(i => i.ExternalUserId).HasMaxLength(255);
    builder.Property(i => i.Status).IsRequired().HasMaxLength(50);

    // Relationship: Integration -> User (N:1)
    builder.HasOne(i => i.User)
           .WithMany(u => u.Integrations) // Assumes ICollection<Integration> exists in User
           .HasForeignKey(i => i.UserId)
           .OnDelete(DeleteBehavior.Cascade); // If user deleted, delete their integrations
  }
}
