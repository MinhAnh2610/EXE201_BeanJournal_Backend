using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Data.Configuration;

public class TemplateConfiguration : IEntityTypeConfiguration<Template>
{
  public void Configure(EntityTypeBuilder<Template> builder)
  {
    builder.HasKey(t => t.TemplateId);

    builder.Property(t => t.Name).IsRequired().HasMaxLength(150);
    builder.Property(t => t.Content).IsRequired();
    builder.Property(t => t.IsPredefined).HasDefaultValue(false);

    // Relationship: Template -> User (N:1, Optional)
    builder.HasOne(t => t.User)
           .WithMany(u => u.Templates) // Assumes ICollection<Template> exists in User
           .HasForeignKey(t => t.UserId) // UserId is nullable
           .IsRequired(false) // Explicitly state FK is optional
           .OnDelete(DeleteBehavior.Cascade); // If user deleted, delete their custom templates
  }
}
