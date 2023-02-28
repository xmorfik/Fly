namespace Fly.Core.Services;

public interface IScheduleService<T>
{
    public Task Schedule(T t);

    public Task Start(int id);

    public Task Stop(int id);
}
