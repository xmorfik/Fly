namespace Fly.Core.Services;

public interface IFilter<T,TParameter>
{
    public Task<IEnumerable<T>> FilterAsync(TParameter parameter);
}
