using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Infrastructure.Repositories;

public class UserRepository(ApplicationDbContext context) : GenericRepository<User>(context), IUserRepository
{
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
    }
  }
}
