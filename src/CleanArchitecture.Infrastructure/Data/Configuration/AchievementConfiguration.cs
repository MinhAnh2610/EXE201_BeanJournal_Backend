using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Data.Configuration;

public class AchievementConfiguration : IEntityTypeConfiguration<Achievement>
{
  public void Configure(EntityTypeBuilder<Achievement> builder)
  {
    builder.HasKey(a => a.AchievementId);

    builder.Property(a => a.Name).IsRequired().HasMaxLength(150);
    builder.Property(a => a.Description).IsRequired();
    builder.Property(a => a.CriteriaKey).IsRequired().HasMaxLength(100);
    builder.HasIndex(a => a.CriteriaKey).IsUnique();

    builder.Property(a => a.BadgeIconUrl).HasMaxLength(255);

    // Relationship: Achievement -> UserAchievements (1:N)
    builder.HasMany(a => a.UserAchievements)
           .WithOne(ua => ua.Achievement)
           .HasForeignKey(ua => ua.AchievementId);
    // Cascade delete behavior handled by UserAchievementConfiguration
  }
}
