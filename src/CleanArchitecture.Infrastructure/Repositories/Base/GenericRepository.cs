using CleanArchitecture.Domain.RepositoryContracts.Base;

namespace CleanArchitecture.Infrastructure.Repositories.Base;

public class GenericRepository<T>(ApplicationDbContext context) : IGenericRepository<T> where T : class
{
  protected readonly ApplicationDbContext _context = context;

  public virtual List<T> GetAll()
  {
    return [.. _context.Set<T>()];
  }

  public virtual async Task<List<T>> GetAllAsync()
  {
    return await _context.Set<T>().ToListAsync();
  }

  public virtual void Create(T entity)
  {
    _context.Set<T>().Add(entity);
  }

  public virtual async Task CreateAsync(T entity)
  {
    await _context.Set<T>().AddAsync(entity);
  }

  public virtual void Update(T entity)
  {
    var tracker = _context.Set<T>().Attach(entity);
    tracker.State = EntityState.Modified;
  }

  public virtual void Remove(T entity)
  {
    var tracker = _context.Set<T>().Attach(entity);
    tracker.State = EntityState.Deleted;
  }

  public virtual T? GetById(int id)
  {
    var entity = _context.Set<T>().Find(id);
    if (entity != null)
    {
      _context.Entry(entity).State = EntityState.Detached;
    }

    return entity;
  }

  public virtual async Task<T?> GetByIdAsync(int id)
  {
    var entity = await _context.Set<T>().FindAsync(id);
    if (entity != null)
    {
      _context.Entry(entity).State = EntityState.Detached;
    }

    return entity;
  }

  public virtual T? GetById(long id)
  {
    var entity = _context.Set<T>().Find(id);
    if (entity != null)
    {
      _context.Entry(entity).State = EntityState.Detached;
    }

    return entity;
  }

  public async Task<T?> GetByIdAsync(long id)
  {
    var entity = await _context.Set<T>().FindAsync(id);
    if (entity != null)
    {
      _context.Entry(entity).State = EntityState.Detached;
    }

    return entity;
  }

  public virtual T? GetById(string code)
  {
    var entity = _context.Set<T>().Find(code);
    if (entity != null)
    {
      _context.Entry(entity).State = EntityState.Detached;
    }

    return entity;
  }

  public virtual async Task<T?> GetByIdAsync(string code)
  {
    var entity = await _context.Set<T>().FindAsync(code);
    if (entity != null)
    {
      _context.Entry(entity).State = EntityState.Detached;
    }

    return entity;
  }

  public virtual T? GetById(Guid code)
  {
    var entity = _context.Set<T>().Find(code);
    if (entity != null)
    {
      _context.Entry(entity).State = EntityState.Detached;
    }

    return entity;
  }

  public virtual async Task<T?> GetByIdAsync(Guid code)
  {
    var entity = await _context.Set<T>().FindAsync(code);
    //if (entity != null)
    //{
    //  _context.Entry(entity).State = EntityState.Detached;
    //}

    return entity;
  }

  public virtual void Attach(T entity)
  {
    ArgumentNullException.ThrowIfNull(entity);

    _context.Attach(entity);
  }

  public virtual IQueryable<T> GetQueryable()
  {
    return _context.Set<T>().AsQueryable();
  }
}