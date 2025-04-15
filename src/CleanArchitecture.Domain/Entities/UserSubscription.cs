using CleanArchitecture.Domain.Abstraction;

namespace CleanArchitecture.Domain.Entities;

public class UserSubscription : Entity<long>
{
  public long UserSubscriptionId { get; set; }
  public long UserId { get; set; }
  public int PlanId { get; set; }
  public string Status { get; set; } = null!;
  public DateTime StartDate { get; set; }
  public DateTime? EndDate { get; set; }
  public string? PaymentGatewayRef { get; set; }

  // Navigation Properties
  public virtual User User { get; set; } = null!;
  public virtual SubscriptionPlan SubscriptionPlan { get; set; } = null!;
}
