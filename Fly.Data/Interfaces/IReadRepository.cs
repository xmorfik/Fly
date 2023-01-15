using Ardalis.Specification;
using Fly.Core.Interfaces;

namespace Fly.Data.Interfaces
{
    public interface IReadRepository<T> : IReadRepositoryBase<T> where T : class, IAggregateRoot
    {
    }
}
