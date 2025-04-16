using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Data.Configuration;

public class UserSettingsConfiguration : IEntityTypeConfiguration<UserSettings>
{
  public void Configure(EntityTypeBuilder<UserSettings> builder)
  {
    // Primary Key is UserId (1:1 relationship with User)
    builder.HasKey(us => us.UserId);

    builder.Property(us => us.FontPreference).HasMaxLength(100);
    builder.Property(us => us.LayoutPreference).HasMaxLength(50);
    builder.Property(us => us.ReminderEnabled).HasDefaultValue(false);

    builder.Property(us => us.UpdatedAt).ValueGeneratedOnAddOrUpdate().HasDefaultValueSql("NOW()");

    // Relationship: UserSettings -> User (1:1)
    builder.HasOne(us => us.User)
           .WithOne(u => u.UserSettings)
           .HasForeignKey<UserSettings>(us => us.UserId)
           .OnDelete(DeleteBehavior.Cascade); // If User deleted, delete their settings

    // Relationship: UserSettings -> Theme (N:1, Optional)
    // Configured on the Theme side, but FK definition needed here if not using convention
    builder.HasOne(us => us.Theme)
           .WithMany(t => t.UserSettings) // Assumes ICollection<UserSettings> exists in Theme
           .HasForeignKey(us => us.ThemeId) // FK is ThemeId
           .IsRequired(false) // ThemeId is nullable
           .OnDelete(DeleteBehavior.SetNull); // If theme deleted, set FK to null
  }
}
