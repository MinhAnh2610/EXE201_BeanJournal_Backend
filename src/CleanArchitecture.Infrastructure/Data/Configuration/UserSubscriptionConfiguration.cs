using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Data.Configuration;

public class UserSubscriptionConfiguration : IEntityTypeConfiguration<UserSubscription>
{
  public void Configure(EntityTypeBuilder<UserSubscription> builder)
  {
    builder.HasKey(us => us.Id);

    builder.Property(us => us.Status).IsRequired().HasMaxLength(50);

    // Relationship: UserSubscription -> User (N:1)
    builder.HasOne(us => us.User)
           .WithMany(u => u.UserSubscriptions)
           .HasForeignKey(us => us.UserId)
           .OnDelete(DeleteBehavior.Cascade); // If user deleted, delete their subscription history

    // Relationship: UserSubscription -> SubscriptionPlan (N:1)
    builder.HasOne(us => us.SubscriptionPlan)
           .WithMany(sp => sp.UserSubscriptions)
           .HasForeignKey(us => us.PlanId)
           .OnDelete(DeleteBehavior.Restrict); // Don't delete a plan if users are subscribed
  }
}
