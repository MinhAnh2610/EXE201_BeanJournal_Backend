using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Infrastructure.Repositories;

public class UserRepository : GenericRepository<User>, IUserRepository
{
  private readonly ILogger<UserRepository> _logger;

  public UserRepository(ApplicationDbContext context, ILogger<UserRepository> logger) : base(context)
  {
    _logger = logger;
  }

  public async Task<User?> GetUserByClerkIdAsync(string clerkId)
  {
    return await _context.Users.FirstOrDefaultAsync(u => u.ClerkUserId == clerkId);
  }

  public async Task<bool> UserExistsByClerkIdAsync(string clerkId)
  {
    return await _context.Users.AnyAsync(u => u.ClerkUserId == clerkId);
  }

  public async Task DeleteUserByClerkIdAsync(string clerkId)
  {
    var user = await GetUserByClerkIdAsync(clerkId);
    if (user != null)
    {
      _context.Users.Remove(user);
      await _context.SaveChangesAsync();
      _logger.LogInformation("Deleted user with Clerk ID: {ClerkId} and internal ID: {UserId}", clerkId, user.UserId);
    }
    else
    {
      _logger.LogWarning("Attempted to delete user with Clerk ID: {ClerkId}, but user was not found.", clerkId);
    }
  }
}
