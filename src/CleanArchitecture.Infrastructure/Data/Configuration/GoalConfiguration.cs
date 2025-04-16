using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Data.Configuration;

public class GoalConfiguration : IEntityTypeConfiguration<Goal>
{
  public void Configure(EntityTypeBuilder<Goal> builder)
  {
    builder.HasKey(g => g.GoalId);

    builder.Property(g => g.Title)
        .IsRequired()
        .HasMaxLength(255);

    builder.Property(g => g.Status)
        .IsRequired()
        .HasMaxLength(50);

    builder.Property(g => g.TargetDate).HasColumnType("date"); // Store only date

    // Relationship: Goal -> User (N:1)
    builder.HasOne(g => g.User)
           .WithMany(u => u.Goals)
           .HasForeignKey(g => g.UserId)
           .OnDelete(DeleteBehavior.Restrict); // If user deleted, delete their goals

    // Relationship: Goal -> GoalUpdates (1:N)
    // Configured implicitly by the GoalUpdate side, but can be specified here too
    builder.HasMany(g => g.GoalUpdates)
           .WithOne(gu => gu.Goal)
           .HasForeignKey(gu => gu.GoalId);
    // Cascade delete for updates is handled by the GoalUpdateConfiguration

  }
}
