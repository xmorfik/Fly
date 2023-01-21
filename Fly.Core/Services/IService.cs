using Fly.Core.Interfaces;

namespace Fly.Core.Services;

public interface IService <T> where T : class, IAggregateDTO
{
    Task CreateAsync(T item);
    Task<T> GetAsync(int id);
    Task UpdateAsync(T item);
    Task DeleteAsync(int id);
    Task<ICollection<T>> GetListAsync();
}
