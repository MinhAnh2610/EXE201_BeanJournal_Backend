using CleanArchitecture.Domain.Abstraction;

namespace CleanArchitecture.Domain.Entities;

public class Entry : Entity<long>
{
  public long UserId { get; set; }
  public string? Title { get; set; }
  public byte[] Content { get; set; } = null!; // Encrypted
  public DateTime EntryDate { get; set; }
  public bool IsPasswordProtected { get; set; } = false;
  public string? EntryPasswordHash { get; set; }
  public decimal? SentimentScore { get; set; }
  public string? DominantMoodLabel { get; set; }

  // Navigation Properties
  public virtual User User { get; set; } = null!;
  public virtual Mood? Mood { get; set; } // 1:1
  public virtual AIAnalysisResult? AIAnalysisResult { get; set; } // 1:1
  public virtual ICollection<Multimedia> Multimedia { get; set; } = new List<Multimedia>(); // 1:N
  public virtual ICollection<EntryTag> EntryTags { get; set; } = new List<EntryTag>(); // N:M junction
  public virtual ICollection<Todo> Todos { get; set; } = new List<Todo>(); // N:1 (optional link from Task)
  public virtual ICollection<GoalUpdate> GoalUpdates { get; set; } = new List<GoalUpdate>(); // N:1 (optional link from GoalUpdate)
}
