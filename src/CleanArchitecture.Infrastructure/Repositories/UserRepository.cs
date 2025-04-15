﻿namespace CleanArchitecture.Infrastructure.Repositories;

public class UserRepository : GenericRepository<User>, IUserRepository
{
  public UserRepository(ApplicationDbContext context) : base(context)
  {
  }
}
