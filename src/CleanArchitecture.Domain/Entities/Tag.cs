using CleanArchitecture.Domain.Abstraction;

namespace CleanArchitecture.Domain.Entities;

public class Tag : Entity<long>
{
  public long TagId { get; set; }
  public long UserId { get; set; }
  public string Name { get; set; } = null!;

  // Navigation Properties
  public virtual User User { get; set; } = null!;
  public virtual ICollection<EntryTag> EntryTags { get; set; } = new List<EntryTag>(); // N:M junction
}
