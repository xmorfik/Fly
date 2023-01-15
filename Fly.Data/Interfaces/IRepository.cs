using Ardalis.Specification;
using Fly.Core.Interfaces;

namespace Fly.Data.Interfaces
{
    public interface IRepository<T> : IRepositoryBase<T> where T : class, IAggregateRoot
    {
    }
}
