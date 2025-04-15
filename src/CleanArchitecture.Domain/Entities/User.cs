using CleanArchitecture.Domain.Abstraction;

namespace CleanArchitecture.Domain.Entities;

public class User : Entity<long>
{
  public string ClerkUserId { get; set; } = null!;
  public long UserId { get; set; }
  public string Email { get; set; } = null!;
  public string? PasswordHash { get; set; }
  public string? Username { get; set; }
  public string? AuthProvider { get; set; }
  public string? AuthProviderId { get; set; }
  public byte[]? EncryptionMasterKey { get; set; }

  // Navigation Properties
  public virtual ICollection<UserSubscription> UserSubscriptions { get; set; } = new List<UserSubscription>();
  public virtual ICollection<Entry> Entries { get; set; } = new List<Entry>();
  public virtual ICollection<Tag> Tags { get; set; } = new List<Tag>();
  public virtual ICollection<Mood> Moods { get; set; } = new List<Mood>();
  public virtual ICollection<Habit> Habits { get; set; } = new List<Habit>();
  public virtual ICollection<HabitLog> HabitLogs { get; set; } = new List<HabitLog>();
  public virtual ICollection<Goal> Goals { get; set; } = new List<Goal>();
  public virtual ICollection<GoalUpdate> GoalUpdates { get; set; } = new List<GoalUpdate>();
  public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();
  public virtual UserSettings? UserSettings { get; set; }
  public virtual ICollection<Streak> Streaks { get; set; } = new List<Streak>();
  public virtual ICollection<UserAchievement> UserAchievements { get; set; } = new List<UserAchievement>();
  public virtual ICollection<Template> Templates { get; set; } = new List<Template>();
  public virtual ICollection<AIAnalysisResult> AIAnalysisResults { get; set; } = new List<AIAnalysisResult>(); // Added for completeness if needed directly
  public virtual ICollection<Integration> Integrations { get; set; } = new List<Integration>();
  public virtual ICollection<Multimedia> Multimedia { get; set; } = new List<Multimedia>(); // Media uploaded by user
}
