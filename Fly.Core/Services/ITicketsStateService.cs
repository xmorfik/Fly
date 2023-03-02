namespace Fly.Core.Services;

public interface ITicketsStateService
{
    public Task SetTicketsStateOnStartFlight(int id);
}
