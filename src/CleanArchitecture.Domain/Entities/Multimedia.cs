using CleanArchitecture.Domain.Abstraction;

namespace CleanArchitecture.Domain.Entities;

public class Multimedia : Entity<long>
{
  public long EntryId { get; set; }
  public long UserId { get; set; } // Owner
  public string StorageUrl { get; set; } = null!;
  public string FileType { get; set; } = null!; // 'image', 'video', 'audio'
  public string? MimeType { get; set; }
  public string? FileName { get; set; }
  public long? SizeBytes { get; set; }

  // Navigation Properties
  public virtual Entry Entry { get; set; } = null!;
  public virtual User User { get; set; } = null!;
}
