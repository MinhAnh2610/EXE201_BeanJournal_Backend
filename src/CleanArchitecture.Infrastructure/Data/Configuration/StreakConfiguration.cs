using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Data.Configuration;

public class StreakConfiguration : IEntityTypeConfiguration<Streak>
{
  public void Configure(EntityTypeBuilder<Streak> builder)
  {
    builder.HasKey(s => s.StreakId);
    builder.Property(s => s.Type).IsRequired().HasMaxLength(50);
    builder.Property(s => s.CurrentLength).HasDefaultValue(0);
    builder.Property(s => s.LongestLength).HasDefaultValue(0);

    // Relationship: Streak -> User (N:1)
    builder.HasOne(s => s.User)
           .WithMany(u => u.Streaks)
           .HasForeignKey(s => s.UserId)
           .OnDelete(DeleteBehavior.Cascade);

    // Relationship: Streak -> Habit (N:1, Optional)
    builder.HasOne(s => s.Habit) // Assumes navigation property exists
           .WithMany(h => h.Streaks) // Assumes navigation property exists
           .HasForeignKey(s => s.RelatedHabitId) // Assumes RelatedHabitId column exists and is FK
           .OnDelete(DeleteBehavior.Cascade); // If Habit deleted, delete related streak

    // Unique constraint: One streak record per user/type/habit(if applicable)
    builder.HasIndex(s => new { s.UserId, s.Type, s.RelatedHabitId }).IsUnique();
  }
}
