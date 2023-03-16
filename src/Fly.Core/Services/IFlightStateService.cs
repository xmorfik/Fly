namespace Fly.Core.Services;

public interface IFlightStateService
{
    public Task Start(int id);

    public Task End(int id);
}
