using CleanArchitecture.Domain.Abstraction;

namespace CleanArchitecture.Domain.Entities;

public class Habit : Entity<long>
{
  public long HabitId { get; set; }
  public long UserId { get; set; }
  public string Name { get; set; } = null!;
  public string? Description { get; set; }
  public string? Frequency { get; set; }
  public string? ColorCode { get; set; }
  public string? Icon { get; set; }

  // Navigation Properties
  public virtual User User { get; set; } = null!;
  public virtual ICollection<HabitLog> HabitLogs { get; set; } = new List<HabitLog>();
  public virtual ICollection<Streak> Streaks { get; set; } = new List<Streak>(); // Link to related streaks
}
