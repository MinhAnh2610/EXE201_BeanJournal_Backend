namespace CleanArchitecture.Domain.Entities;

public class EntryTag
{
  public long EntryId { get; set; }
  public long TagId { get; set; }
  public DateTime AssignedAt { get; set; } = DateTime.UtcNow;

  // Navigation Properties
  public virtual Entry Entry { get; set; } = null!;
  public virtual Tag Tag { get; set; } = null!;
}
