using CleanArchitecture.Domain.Abstraction;

namespace CleanArchitecture.Domain.Entities;

public class Template : Entity<long>
{
  public long? UserId { get; set; } // Null for predefined
  public string Name { get; set; } = null!;
  public string Content { get; set; } = null!;
  public bool IsPredefined { get; set; } = false;

  // Navigation Properties
  public virtual User? User { get; set; }
}
