using Fly.Core.Interfaces;
using Fly.Core.Pagination;

namespace Fly.Core.Services;

// to do: return item on create, return info on delete, update and list 
public interface IService<T, TParameter> where T : class, IAggregateEntities
{
    public Task CreateAsync(T item);
    public Task UpdateAsync(T item);
    public Task DeleteAsync(int id);
    public Task<ResponseBase<T>> GetAsync(int id);
    public Task<PagedResponse<T>> GetListAsync(TParameter parameter, Page page);
}
