using CleanArchitecture.Application.ServiceContracts;
using CleanArchitecture.Domain.RepositoryContracts.UnitOfWork;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Infrastructure.Repositories.UnitOfWork;

public class UnitOfWork(ApplicationDbContext context, ILogger<UnitOfWork> logger, ITimeZoneService timeZoneService) : IUnitOfWork
{
  private readonly ApplicationDbContext _context = context;
  private IDbContextTransaction _transaction;
  //private readonly bool _commited;
  private readonly ILogger<UnitOfWork> _logger = logger;
  private readonly ITimeZoneService _timeZoneService = timeZoneService;

  #region Repositories
  public IUserRepository Users => new UserRepository(_context);
  #endregion

  public async Task RollBackAsync()
  {
    await _transaction.RollbackAsync();
  }

  public async Task<bool> CompleteAsync(CancellationToken cancellationToken = default)
  {
    try
    {
      await _context.SaveChangesAsync(cancellationToken);
      await _transaction.CommitAsync();
      return true;
    }
    catch (Exception ex)
    {
       await _transaction.RollbackAsync();
      _logger.LogError($"Database saved failed at {_timeZoneService.ConvertToLocalTime(DateTime.UtcNow)}\n" +
                       $"with error: {ex.Message}");
      return false;
    }
  }
  public async Task<IDbContextTransaction> BeginTransactionAsync()
  {
    _transaction = await _context.Database.BeginTransactionAsync();
    return _transaction;
  }
  public void Dispose()
  {
    _transaction?.Dispose();
    _transaction = null;
  }
}