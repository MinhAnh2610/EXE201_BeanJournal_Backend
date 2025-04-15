using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Reflection;

namespace CleanArchitecture.Infrastructure.Data;

public interface IApplicationDbContext
{
  Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
  public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
  {
  }

  public ApplicationDbContext()
  {
  }

  public DbSet<User> Users { get; set; } = null!;
  public DbSet<SubscriptionPlan> SubscriptionPlans { get; set; } = null!;
  public DbSet<UserSubscription> UserSubscriptions { get; set; } = null!;
  public DbSet<Entry> Entries { get; set; } = null!;
  public DbSet<Multimedia> Multimedia { get; set; } = null!;
  public DbSet<Tag> Tags { get; set; } = null!;
  public DbSet<EntryTag> EntryTags { get; set; } = null!; // Include junction entity if needed directly
  public DbSet<Mood> Moods { get; set; } = null!;
  public DbSet<Habit> Habits { get; set; } = null!;
  public DbSet<HabitLog> HabitLogs { get; set; } = null!;
  public DbSet<Goal> Goals { get; set; } = null!;
  public DbSet<GoalUpdate> GoalUpdates { get; set; } = null!;
  public DbSet<Todo> Todos { get; set; } = null!;
  public DbSet<Theme> Themes { get; set; } = null!;
  public DbSet<UserSettings> UserSettings { get; set; } = null!;
  public DbSet<Streak> Streaks { get; set; } = null!;
  public DbSet<Achievement> Achievements { get; set; } = null!;
  public DbSet<UserAchievement> UserAchievements { get; set; } = null!; // Include junction entity if needed directly
  public DbSet<Template> Templates { get; set; } = null!;
  public DbSet<AIAnalysisResult> AIAnalysisResults { get; set; } = null!;
  public DbSet<Integration> Integrations { get; set; } = null!;

  protected override void OnModelCreating(ModelBuilder builder)
  {
    builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    base.OnModelCreating(builder);
  }
}
