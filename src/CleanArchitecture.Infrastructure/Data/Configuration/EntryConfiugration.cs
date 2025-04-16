using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Data.Configuration;

public class EntryConfiugration : IEntityTypeConfiguration<Entry>
{
  public void Configure(EntityTypeBuilder<Entry> builder)
  {
    builder.HasKey(e => e.Id);

    builder.Property(e => e.Title).HasMaxLength(255);
    builder.Property(e => e.Content).IsRequired(); // Assuming BLOB or TEXT type handled by provider
    builder.Property(e => e.EntryDate).IsRequired();
    builder.HasIndex(e => e.EntryDate); // Index for sorting/filtering by date

    builder.Property(e => e.IsPasswordProtected).HasDefaultValue(false);
    builder.Property(e => e.EntryPasswordHash).HasMaxLength(255);

    builder.Property(e => e.SentimentScore).HasColumnType("decimal(3,2)");
    builder.Property(e => e.DominantMoodLabel).HasMaxLength(50);

    // Relationship: Entry -> User (N:1)
    builder.HasOne(e => e.User)
           .WithMany(u => u.Entries)
           .HasForeignKey(e => e.UserId)
           .OnDelete(DeleteBehavior.Cascade); // If User is deleted, delete their entries

    // Relationships for 1:1 (Mood, AIAnalysisResult) and 1:N (Multimedia)
    // are configured from the other side or handled by convention if FKs are present.
    // N:M (EntryTags) configured separately.
  }
}
