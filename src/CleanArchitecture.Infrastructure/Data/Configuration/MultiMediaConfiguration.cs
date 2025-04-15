using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Data.Configuration;

public class MultiMediaConfiguration : IEntityTypeConfiguration<Multimedia>
{
  public void Configure(EntityTypeBuilder<Multimedia> builder)
  {
    builder.HasKey(m => m.MediaId);

    builder.Property(m => m.StorageUrl).IsRequired().HasMaxLength(512);
    builder.Property(m => m.FileType).IsRequired().HasMaxLength(20);
    builder.Property(m => m.MimeType).HasMaxLength(100);
    builder.Property(m => m.FileName).HasMaxLength(255);

    // Relationship: Multimedia -> Entry (N:1)
    builder.HasOne(m => m.Entry)
           .WithMany(e => e.Multimedia)
           .HasForeignKey(m => m.EntryId)
           .OnDelete(DeleteBehavior.Cascade); // If Entry deleted, delete associated media

    // Relationship: Multimedia -> User (N:1) - Explicit Owner
    builder.HasOne(m => m.User)
           .WithMany(u => u.Multimedia) // Add ICollection<Multimedia> to User if needed
           .HasForeignKey(m => m.UserId)
           .OnDelete(DeleteBehavior.Restrict); // Or Cascade, depending on requirement - maybe restrict if user deleted?
  }
}
