using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Data.Configuration;

public class TaskConfiguration : IEntityTypeConfiguration<Domain.Entities.Task>
{
  public void Configure(EntityTypeBuilder<Domain.Entities.Task> builder)
  {
    builder.HasKey(t => t.TaskId);

    builder.Property(t => t.Description).IsRequired();
    builder.Property(t => t.IsCompleted).HasDefaultValue(false);
    builder.Property(t => t.DueDate).HasColumnType("date");

    // Relationship: Task -> User (N:1)
    builder.HasOne(t => t.User)
           .WithMany(u => u.Tasks)
           .HasForeignKey(t => t.UserId)
           .OnDelete(DeleteBehavior.Cascade); // If user deleted, delete their tasks

    // Relationship: Task -> Entry (N:1, Optional)
    builder.HasOne(t => t.Entry)
           .WithMany(e => e.Tasks) // Assumes ICollection<Task> exists in Entry
           .HasForeignKey(t => t.EntryId)
           .OnDelete(DeleteBehavior.SetNull); // If Entry deleted, just remove the link from the task

  }
}
