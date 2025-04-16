using CleanArchitecture.Domain.Abstraction;

namespace CleanArchitecture.Domain.Entities;

public class Integration : Entity<long>
{
  public long UserId { get; set; }
  public string ServiceName { get; set; } = null!; // 'google_calendar', 'notion', etc.
  public byte[] AccessTokenEncrypted { get; set; } = null!; // Encrypted
  public byte[]? RefreshTokenEncrypted { get; set; } // Encrypted
  public string? ExternalUserId { get; set; }
  public string? Scopes { get; set; }
  public string Status { get; set; } = null!; // 'active', 'revoked'

  // Navigation Properties
  public virtual User User { get; set; } = null!;
}
