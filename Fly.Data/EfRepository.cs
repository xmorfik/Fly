using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Fly.Core.Interfaces;
using Fly.Data.Interfaces;

namespace Fly.Data;

public class EfRepository<T> : RepositoryBase<T>, IReadRepository<T>, IRepository<T> where T : class, IAggregateRoot
{
    public EfRepository(FlyDbContext dbContext) : base(dbContext)
    {
    }
}