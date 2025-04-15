using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Data.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
  public void Configure(EntityTypeBuilder<User> builder)
  {
    builder.HasKey(u => u.UserId); // Keep internal PK

    // Configure the ClerkUserId
    builder.Property(u => u.ClerkUserId)
        .IsRequired()
        .HasMaxLength(100); // Adjust as needed
    builder.HasIndex(u => u.ClerkUserId).IsUnique(); // MUST be unique

    // Configure optional synced fields
    builder.Property(u => u.Email).HasMaxLength(255);
    // Optional: Index email if you query by it locally, but it's not unique here anymore
    // builder.HasIndex(u => u.Email);

    builder.Property(u => u.Username).HasMaxLength(100);

    // Remove configuration for PasswordHash, AuthProvider, AuthProviderId

    // All existing relationship configurations originating FROM User remain the same,
    // using the internal UserId PK.
    // Relationships pointing TO User from other tables also use the internal UserId PK.

  }
}
