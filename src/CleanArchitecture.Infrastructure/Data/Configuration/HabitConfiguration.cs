using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Data.Configuration;

public class HabitConfiguration : IEntityTypeConfiguration<Habit>
{
  public void Configure(EntityTypeBuilder<Habit> builder)
  {
    builder.HasKey(h => h.HabitId);
    builder.Property(h => h.Name).IsRequired().HasMaxLength(150);
    builder.Property(h => h.Frequency).HasMaxLength(50);
    builder.Property(h => h.ColorCode).HasMaxLength(7);
    builder.Property(h => h.Icon).HasMaxLength(50);
    builder.Property(h => h.IsActive).HasDefaultValue(true);

    // Relationship: Habit -> User (N:1)
    builder.HasOne(h => h.User)
           .WithMany(u => u.Habits)
           .HasForeignKey(h => h.UserId)
           .OnDelete(DeleteBehavior.Cascade); // Delete habits if user is deleted
  }
}
