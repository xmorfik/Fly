namespace Fly.Core.Interfaces;

public interface GpsService<T>
{
    public Task<IEnumerable<T>> GetAll();
    public Task<T> Get(int id);
}
