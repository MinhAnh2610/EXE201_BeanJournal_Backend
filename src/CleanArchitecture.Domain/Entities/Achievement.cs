using CleanArchitecture.Domain.Abstraction;

namespace CleanArchitecture.Domain.Entities;

public class Achievement : Entity<int>
{
  public int AchievementId { get; set; }
  public string Name { get; set; } = null!;
  public string Description { get; set; } = null!;
  public string CriteriaKey { get; set; } = null!; // e.g., 'journal_streak_10'
  public string? BadgeIconUrl { get; set; }

  // Navigation Properties
  public virtual ICollection<UserAchievement> UserAchievements { get; set; } = new List<UserAchievement>();
}
