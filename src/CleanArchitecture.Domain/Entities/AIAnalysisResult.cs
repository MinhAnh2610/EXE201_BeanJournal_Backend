namespace CleanArchitecture.Domain.Entities;

public class AIAnalysisResult
{
  public long EntryId { get; set; } // PK and FK
  public string Type { get; set; } = null!; // 'sentiment', 'keywords', etc.
  public string ResultJson { get; set; } = null!; // JSON stored as string
  public long UserId { get; set; } // Added for direct query convenience

  // Navigation Properties
  public virtual Entry Entry { get; set; } = null!;
  public virtual User User { get; set; } = null!; // Added for direct query convenience
}
