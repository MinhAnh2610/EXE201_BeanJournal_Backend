namespace CleanArchitecture.Domain.Entities;

public class UserSettings
{
  public long UserId { get; set; } // PK and FK
  public int? ThemeId { get; set; } // Optional FK
  public string? FontPreference { get; set; }
  public string? LayoutPreference { get; set; }
  public bool ReminderEnabled { get; set; } = false;
  public TimeSpan? ReminderTime { get; set; } // Use TimeSpan for time of day
  public string? OtherSettingsJson { get; set; } // JSON stored as string
  public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

  // Navigation Properties
  public virtual User User { get; set; } = null!;
  public virtual Theme? Theme { get; set; }
}
