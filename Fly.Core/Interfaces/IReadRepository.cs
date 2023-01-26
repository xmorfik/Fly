using Ardalis.Specification;

namespace Fly.Core.Interfaces;

public interface IReadRepository<T> : IReadRepositoryBase<T> where T : class, IAggregateEntities
{
}
