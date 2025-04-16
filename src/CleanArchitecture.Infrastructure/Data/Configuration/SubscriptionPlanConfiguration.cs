using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Data.Configuration;

public class SubscriptionPlanConfiguration : IEntityTypeConfiguration<SubscriptionPlan>
{
  public void Configure(EntityTypeBuilder<SubscriptionPlan> builder)
  {
    builder.HasKey(sp => sp.Id);

    builder.Property(sp => sp.PlanCode)
        .IsRequired()
        .HasMaxLength(50);
    builder.HasIndex(sp => sp.PlanCode).IsUnique();

    builder.Property(sp => sp.Name)
        .IsRequired()
        .HasMaxLength(100);

    builder.Property(sp => sp.PriceMonthly).HasColumnType("decimal(10,2)");
    builder.Property(sp => sp.PriceYearly).HasColumnType("decimal(10,2)");
    builder.Property(sp => sp.BillingCycle).HasMaxLength(20);
  }
}
