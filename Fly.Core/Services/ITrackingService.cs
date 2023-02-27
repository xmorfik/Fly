namespace Fly.Core.Services;

public interface ITrackingService
{
    public Task Track(int id);
    public Task Stop(int id);
    public Task Update();
}
