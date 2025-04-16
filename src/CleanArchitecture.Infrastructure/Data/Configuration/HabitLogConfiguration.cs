using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Data.Configuration;

public class HabitLogConfiguration : IEntityTypeConfiguration<HabitLog>
{
  public void Configure(EntityTypeBuilder<HabitLog> builder)
  {
    builder.HasKey(hl => hl.Id);
    builder.Property(hl => hl.Status).IsRequired().HasMaxLength(20);

    // Relationship: HabitLog -> Habit (N:1)
    builder.HasOne(hl => hl.Habit)
           .WithMany(h => h.HabitLogs)
           .HasForeignKey(hl => hl.HabitId)
           .OnDelete(DeleteBehavior.Cascade); // Delete logs if habit definition is deleted

    // Relationship: HabitLog -> User (N:1)
    builder.HasOne(hl => hl.User)
          .WithMany(u => u.HabitLogs)
          .HasForeignKey(hl => hl.UserId)
          .OnDelete(DeleteBehavior.Restrict); // Maybe don't cascade from user directly?
                                              // Cascade delete from Habit is usually sufficient.
  }
}
