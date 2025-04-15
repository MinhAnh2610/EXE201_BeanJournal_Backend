using CleanArchitecture.Domain.Abstraction;

namespace CleanArchitecture.Domain.Entities;

public class GoalUpdate : Entity<long>
{
  public long GoalUpdateId { get; set; }
  public long GoalId { get; set; }
  public long UserId { get; set; }
  public long? EntryId { get; set; } // Optional link
  public string? UpdateText { get; set; }
  public string? ProgressMetric { get; set; }

  // Navigation Properties
  public virtual Goal Goal { get; set; } = null!;
  public virtual User User { get; set; } = null!;
  public virtual Entry? Entry { get; set; }
}
