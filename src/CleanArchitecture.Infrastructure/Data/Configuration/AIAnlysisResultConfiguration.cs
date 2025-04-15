using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Data.Configuration;

public class AIAnlysisResultConfiguration : IEntityTypeConfiguration<AIAnalysisResult>
{
  public void Configure(EntityTypeBuilder<AIAnalysisResult> builder)
  {
    // Primary Key is EntryId (1:1 relationship)
    builder.HasKey(a => a.EntryId);

    builder.Property(a => a.Type).IsRequired().HasMaxLength(50);
    builder.Property(a => a.ResultJson).IsRequired(); // Assuming JSON stored as string/TEXT

    // Relationship: AIAnalysisResult -> Entry (1:1) - FK defined by PK
    builder.HasOne(a => a.Entry)
           .WithOne(e => e.AIAnalysisResult)
           .HasForeignKey<AIAnalysisResult>(a => a.EntryId)
           .OnDelete(DeleteBehavior.Cascade); // If Entry deleted, delete AI result

    // Relationship: AIAnalysisResult -> User (N:1)
    builder.HasOne(a => a.User)
        .WithMany(u => u.AIAnalysisResults) // Add ICollection to User if needed
        .HasForeignKey(a => a.UserId)
        .OnDelete(DeleteBehavior.Restrict); // Avoid deleting user if analysis results exist?
  }
}
