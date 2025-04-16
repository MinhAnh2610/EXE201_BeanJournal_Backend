using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Data.Configuration;

public class TagConfiguration : IEntityTypeConfiguration<Tag>
{
  public void Configure(EntityTypeBuilder<Tag> builder)
  {
    builder.HasKey(t => t.TagId);

    builder.Property(t => t.Name).IsRequired().HasMaxLength(100);

    // Relationship: Tag -> User (N:1)
    builder.HasOne(t => t.User)
           .WithMany(u => u.Tags)
           .HasForeignKey(t => t.UserId)
           .OnDelete(DeleteBehavior.Restrict); // If User deleted, delete their tags

    // Unique constraint: Tag name must be unique per user
    builder.HasIndex(t => new { t.UserId, t.Name }).IsUnique();
  }
}
