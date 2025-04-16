using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Data.Configuration;

public class ThemeConfiguration : IEntityTypeConfiguration<Theme>
{
  public void Configure(EntityTypeBuilder<Theme> builder)
  {
    builder.HasKey(t => t.Id);

    builder.Property(t => t.Name)
        .IsRequired()
        .HasMaxLength(100);
    builder.HasIndex(t => t.Name).IsUnique();

    builder.Property(t => t.StylePropertiesJson).IsRequired();
    builder.Property(t => t.IsPremium).HasDefaultValue(false);
    builder.Property(t => t.IsPredefined).HasDefaultValue(true);

    // Relationship: Theme -> UserSettings (1:N)
    builder.HasMany(t => t.UserSettings)
           .WithOne(us => us.Theme)
           .HasForeignKey(us => us.ThemeId)
           .OnDelete(DeleteBehavior.SetNull); // If Theme deleted, set ThemeId in UserSettings to NULL
  }
}
