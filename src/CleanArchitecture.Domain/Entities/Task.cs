using CleanArchitecture.Domain.Abstraction;

namespace CleanArchitecture.Domain.Entities;

public class Task : Entity<long>
{
  public long TaskId { get; set; }
  public long UserId { get; set; }
  public long? EntryId { get; set; } // Optional link
  public string Description { get; set; } = null!;
  public bool IsCompleted { get; set; } = false;
  public DateTime? DueDate { get; set; }
  public DateTime? CompletedAt { get; set; }

  // Navigation Properties
  public virtual User User { get; set; } = null!;
  public virtual Entry? Entry { get; set; }
}
