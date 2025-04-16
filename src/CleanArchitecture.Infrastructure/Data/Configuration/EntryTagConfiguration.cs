using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Data.Configuration;

public class EntryTagConfiguration : IEntityTypeConfiguration<EntryTag>
{
  public void Configure(EntityTypeBuilder<EntryTag> builder)
  {
    // Composite Primary Key
    builder.HasKey(et => new { et.EntryId, et.TagId });

    // Relationship: EntryTag -> Entry (N:1)
    builder.HasOne(et => et.Entry)
           .WithMany(e => e.EntryTags)
           .HasForeignKey(et => et.EntryId)
           .OnDelete(DeleteBehavior.Cascade); // If Entry deleted, remove tag association

    // Relationship: EntryTag -> Tag (N:1)
    builder.HasOne(et => et.Tag)
           .WithMany(t => t.EntryTags)
           .HasForeignKey(et => et.TagId)
           .OnDelete(DeleteBehavior.Cascade); // If Tag deleted, remove tag association

    builder.Property(et => et.AssignedAt).ValueGeneratedOnAdd().HasDefaultValueSql("NOW()");
  }
}
