namespace CleanArchitecture.Domain.Entities;

public class UserAchievement
{
  public long UserId { get; set; }
  public int AchievementId { get; set; }
  public DateTime EarnedAt { get; set; } = DateTime.UtcNow;

  // Navigation Properties
  public virtual User User { get; set; } = null!;
  public virtual Achievement Achievement { get; set; } = null!;
}
