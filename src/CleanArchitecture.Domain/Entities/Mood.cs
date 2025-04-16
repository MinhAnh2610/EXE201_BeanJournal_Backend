using CleanArchitecture.Domain.Abstraction;

namespace CleanArchitecture.Domain.Entities;

public class Mood
{
  public long EntryId { get; set; } // PK and FK
  public long UserId { get; set; }
  public string Label { get; set; } = null!;
  public decimal? Score { get; set; }
  public string Source { get; set; } = null!; // 'manual' or 'ai'
  public string? Notes { get; set; }

  // Navigation Properties
  public virtual Entry Entry { get; set; } = null!;
  public virtual User User { get; set; } = null!;
}
