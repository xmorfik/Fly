namespace Fly.Core.Services;
public interface IAircraftLocationService<T>
{
    public Task<ICollection<T>> GetСurrentLocations();
    public Task<ICollection<T>> GetLocations(int id);
}
