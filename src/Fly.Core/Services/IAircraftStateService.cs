namespace Fly.Core.Services;

public interface IAircraftStateService
{
    Task Takeoff(int id);

    Task Landing(int id, int airportId);
}
