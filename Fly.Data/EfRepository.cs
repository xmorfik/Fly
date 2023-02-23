using Ardalis.Specification.EntityFrameworkCore;
using Fly.Core.Interfaces;

namespace Fly.Data;

public class EfRepository<T> : RepositoryBase<T>, IReadRepository<T>, IRepository<T> where T : class, IAggregateEntities
{
    public EfRepository(FlyDbContext dbContext) : base(dbContext)
    {
    }
}