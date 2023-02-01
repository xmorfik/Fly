namespace Fly.Core.Services;
public interface IAircraftLocationService<T>
{
    public Task<ICollection<T>> GetСurrentLocations();
    public Task<ICollection<T>> GetLocations(int id);
    public Task CreateAsync(T item);
    public Task UpdateAsync(T item);
    public Task DeleteAsync(int id);
    public Task DeleteHistoryAsync(int id);
}
