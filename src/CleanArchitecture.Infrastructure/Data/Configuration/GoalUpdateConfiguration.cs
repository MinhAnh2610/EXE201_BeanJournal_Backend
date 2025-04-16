using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Data.Configuration;

public class GoalUpdateConfiguration : IEntityTypeConfiguration<GoalUpdate>
{
  public void Configure(EntityTypeBuilder<GoalUpdate> builder)
  {
    builder.HasKey(gu => gu.Id);

    builder.Property(gu => gu.ProgressMetric).HasMaxLength(100);

    // Relationship: GoalUpdate -> Goal (N:1)
    builder.HasOne(gu => gu.Goal)
           .WithMany(g => g.GoalUpdates)
           .HasForeignKey(gu => gu.GoalId)
           .OnDelete(DeleteBehavior.Cascade); // If Goal deleted, delete its updates

    // Relationship: GoalUpdate -> User (N:1)
    builder.HasOne(gu => gu.User)
           .WithMany(u => u.GoalUpdates)
           .HasForeignKey(gu => gu.UserId)
           .OnDelete(DeleteBehavior.Restrict); // Prevent user delete if updates exist? Or Cascade? Choose carefully. Cascade from Goal might be enough.

    // Relationship: GoalUpdate -> Entry (N:1, Optional)
    builder.HasOne(gu => gu.Entry)
           .WithMany(e => e.GoalUpdates) // Assumes ICollection<GoalUpdate> exists in Entry
           .HasForeignKey(gu => gu.EntryId)
           .OnDelete(DeleteBehavior.SetNull); // If Entry deleted, just remove the link from the update
  }
}
