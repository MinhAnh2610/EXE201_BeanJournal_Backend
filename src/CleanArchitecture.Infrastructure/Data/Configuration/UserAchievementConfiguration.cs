using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Data.Configuration;

public class UserAchievementConfiguration : IEntityTypeConfiguration<UserAchievement>
{
  public void Configure(EntityTypeBuilder<UserAchievement> builder)
  {
    builder.HasKey(ua => new { ua.UserId, ua.AchievementId }); // Composite Key

    builder.Property(ua => ua.EarnedAt).ValueGeneratedOnAdd().HasDefaultValueSql("NOW()");

    // Relationship: UserAchievement -> User (N:1)
    builder.HasOne(ua => ua.User)
          .WithMany(u => u.UserAchievements)
          .HasForeignKey(ua => ua.UserId)
          .OnDelete(DeleteBehavior.Cascade); // Remove achievement record if user deleted

    // Relationship: UserAchievement -> Achievement (N:1)
    builder.HasOne(ua => ua.Achievement)
           .WithMany(a => a.UserAchievements)
           .HasForeignKey(ua => ua.AchievementId)
           .OnDelete(DeleteBehavior.Cascade); // Remove record if achievement definition deleted
  }
}
