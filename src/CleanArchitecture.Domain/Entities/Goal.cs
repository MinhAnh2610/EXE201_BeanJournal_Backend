using CleanArchitecture.Domain.Abstraction;

namespace CleanArchitecture.Domain.Entities;

public class Goal : Entity<long>
{
  public long GoalId { get; set; }
  public long UserId { get; set; }
  public string Title { get; set; } = null!;
  public string? Description { get; set; }
  public string Status { get; set; } = null!; // 'active', 'completed', 'archived'
  public DateTime? TargetDate { get; set; }

  // Navigation Properties
  public virtual User User { get; set; } = null!;
  public virtual ICollection<GoalUpdate> GoalUpdates { get; set; } = new List<GoalUpdate>();
}
