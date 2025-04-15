using CleanArchitecture.Domain.Abstraction;

namespace CleanArchitecture.Domain.Entities;

public class Theme : Entity<int>
{
  public int ThemeId { get; set; }
  public string Name { get; set; } = null!;
  public string? Description { get; set; }
  public string StylePropertiesJson { get; set; } = null!; // JSON stored as string
  public bool IsPremium { get; set; } = false;
  public bool IsPredefined { get; set; } = true;

  // Navigation Properties
  public virtual ICollection<UserSettings> UserSettings { get; set; } = new List<UserSettings>();
}
