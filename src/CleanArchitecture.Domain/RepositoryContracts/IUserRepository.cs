using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.RepositoryContracts.Base;

namespace CleanArchitecture.Domain.RepositoryContracts;

public interface IUserRepository : IGenericRepository<User>
{
  Task<User?> GetUserByClerkIdAsync(string clerkId);
  Task DeleteUserByClerkIdAsync(string clerkId);
  Task<bool> UserExistsByClerkIdAsync(string clerkId); // Helper
}
