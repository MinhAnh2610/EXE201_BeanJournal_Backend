using CleanArchitecture.Domain.Abstraction;

namespace CleanArchitecture.Domain.Entities;

public class HabitLog : Entity<long>
{
  public long HabitId { get; set; }
  public long UserId { get; set; }
  public string Status { get; set; } = null!; // 'completed', 'skipped'
  public string? Notes { get; set; }

  // Navigation Properties
  public virtual Habit Habit { get; set; } = null!;
  public virtual User User { get; set; } = null!;
}
