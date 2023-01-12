using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Finance.Core.Interfaces;

namespace Fly.Data;

public class EfRepository<T> : RepositoryBase<T>, IReadRepositoryBase<T>, IRepositoryBase<T> where T : class, IAggregateRoot
{
    public EfRepository(FlyDbContext dbContext) : base(dbContext)
    {
    }
}