using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Data.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
  public void Configure(EntityTypeBuilder<User> builder)
  {
    builder.HasKey(u => u.UserId); // Keep internal PK

    // Configure the ClerkUserId
    builder.Property(u => u.ClerkUserId)
        .IsRequired()
        .HasMaxLength(100); // Adjust as needed
    builder.HasIndex(u => u.ClerkUserId).IsUnique(); // MUST be unique

    // Configure optional synced fields
    builder.Property(u => u.Email).HasMaxLength(255);
    // Optional: Index email if you query by it locally, but it's not unique here anymore
    // builder.HasIndex(u => u.Email);

    builder.Property(u => u.Username).HasMaxLength(100);

    // Remove configuration for PasswordHash, AuthProvider, AuthProviderId

    // All existing relationship configurations originating FROM User remain the same,
    // using the internal UserId PK.
    // Relationships pointing TO User from other tables also use the internal UserId PK.
    builder.HasMany(u => u.UserSubscriptions)
               .WithOne(us => us.User)
               .HasForeignKey(us => us.UserId);

    builder.HasMany(u => u.Entries)
           .WithOne(e => e.User)
           .HasForeignKey(e => e.UserId);

    builder.HasMany(u => u.Tags)
           .WithOne(t => t.User)
           .HasForeignKey(t => t.UserId);

    builder.HasMany(u => u.Moods)
           .WithOne(m => m.User)
           .HasForeignKey(m => m.UserId);

    builder.HasMany(u => u.Habits)
           .WithOne(h => h.User)
           .HasForeignKey(h => h.UserId);

    builder.HasMany(u => u.HabitLogs)
           .WithOne(hl => hl.User)
           .HasForeignKey(hl => hl.UserId);

    builder.HasMany(u => u.Goals)
           .WithOne(g => g.User)
           .HasForeignKey(g => g.UserId);

    builder.HasMany(u => u.GoalUpdates)
           .WithOne(gu => gu.User)
           .HasForeignKey(gu => gu.UserId);

    builder.HasMany(u => u.Todos)
           .WithOne(t => t.User)
           .HasForeignKey(t => t.UserId);

    builder.HasOne(u => u.UserSettings)
           .WithOne(us => us.User)
           .HasForeignKey<UserSettings>(us => us.UserId);

    builder.HasMany(u => u.Streaks)
           .WithOne(s => s.User)
           .HasForeignKey(s => s.UserId);

    builder.HasMany(u => u.UserAchievements)
           .WithOne(ua => ua.User)
           .HasForeignKey(ua => ua.UserId);

    builder.HasMany(u => u.Templates)
           .WithOne(t => t.User)
           .HasForeignKey(t => t.UserId);

    builder.HasMany(u => u.AIAnalysisResults)
           .WithOne(ar => ar.User)
           .HasForeignKey(ar => ar.UserId);

    builder.HasMany(u => u.Integrations)
           .WithOne(i => i.User)
           .HasForeignKey(i => i.UserId);

    builder.HasMany(u => u.Multimedia)
           .WithOne(m => m.User)
           .HasForeignKey(m => m.UserId);
  }
}
