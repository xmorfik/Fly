using Ardalis.Specification;

namespace Fly.Core.Interfaces;

public interface IRepository<T> : IRepositoryBase<T> where T : class, IAggregateRoot
{
}
