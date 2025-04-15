using CleanArchitecture.Domain.Abstraction;

namespace CleanArchitecture.Domain.Entities;

public class SubscriptionPlan : Entity<int>
{

  public int SubscriptionPlanId { get; set; }
  public string PlanCode { get; set; } = null!;
  public string Name { get; set; } = null!;
  public string? Description { get; set; }
  public decimal? PriceMonthly { get; set; }
  public decimal? PriceYearly { get; set; }
  public string? BillingCycle { get; set; }
  public string? FeatureFlags { get; set; } // JSON stored as string

  // Navigation Properties
  public virtual ICollection<UserSubscription> UserSubscriptions { get; set; } = new List<UserSubscription>();
}
