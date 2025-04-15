using Microsoft.EntityFrameworkCore.Storage;

namespace CleanArchitecture.Domain.RepositoryContracts.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
  #region Repositories
  IUserRepository Users { get; }
  #endregion

  Task<IDbContextTransaction> BeginTransactionAsync();
  Task RollBackAsync();
  Task<bool> CompleteAsync();
}
