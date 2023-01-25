using Fly.Core.Interfaces;
using Fly.Core.Pagination;

namespace Fly.Core.Services;

public interface IService <T,TResult, TParameter> where T : class, IAggregateDTO
{
    public Task CreateAsync(T item);
    public Task<Response<T>> GetAsync(int id);
    public Task UpdateAsync(T item);
    public Task DeleteAsync(int id);
    public Task<TResult> GetListAsync(TParameter parameter, Page page);
}
