using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Data.Configuration;

public class MoodConfiguration : IEntityTypeConfiguration<Mood>
{
  public void Configure(EntityTypeBuilder<Mood> builder)
  {
    // Primary Key is EntryId (1:1 relationship)
    builder.HasKey(m => m.EntryId);

    builder.Property(m => m.Label).IsRequired().HasMaxLength(50);
    builder.Property(m => m.Score).HasColumnType("decimal(3,2)");
    builder.Property(m => m.Source).IsRequired().HasMaxLength(20);

    // Relationship: Mood -> Entry (1:1) - FK defined by PK
    // Configuration primarily handled by HasKey referencing the FK property
    builder.HasOne(m => m.Entry)
           .WithOne(e => e.Mood)
           .HasForeignKey<Mood>(m => m.EntryId) // Explicitly define FK if needed
           .OnDelete(DeleteBehavior.Cascade); // If Entry deleted, delete Mood record

    // Relationship: Mood -> User (N:1)
    builder.HasOne(m => m.User)
           .WithMany(u => u.Moods)
           .HasForeignKey(m => m.UserId)
           // Decide delete behavior - Restrict might be safer than Cascading from User
           .OnDelete(DeleteBehavior.Restrict); // Don't delete user if mood logs exist?

  }
}
