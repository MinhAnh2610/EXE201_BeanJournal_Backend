using CleanArchitecture.Domain.Abstraction;

namespace CleanArchitecture.Domain.Entities;

public class Streak : Entity<long>
{
  public long UserId { get; set; }
  public string Type { get; set; } = null!; // 'journaling', 'habit'
  public long? RelatedHabitId { get; set; } // Added back - FK to Habit, NULL for journaling
  public int CurrentLength { get; set; } = 0;
  public int LongestLength { get; set; } = 0;

  // Navigation Properties
  public virtual User User { get; set; } = null!;
  public virtual Habit? Habit { get; set; } // Link back if it's a habit streak
}
