using Fly.Core.Interfaces;
using Fly.Core.Pagination;

namespace Fly.Core.Services;

public interface IService<T, TParameter> where T : class, IAggregateEntities
{
    public Task CreateAsync(T item);
    public Task UpdateAsync(T item);
    public Task DeleteAsync(int id);
    public Task<Response<T>> GetAsync(int id);
    public Task<PagedResponse<T>> GetListAsync(TParameter parameter, Page page);
}
