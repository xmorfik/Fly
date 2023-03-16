namespace Fly.Core.Services;
public interface IAircraftLocationService<T>
{
    public Task<IEnumerable<T>> GetСurrentLocations();
    public Task CreateAsync(T item);
    public Task UpdateAsync(T item);
    public Task DeleteAsync(int id);
}
