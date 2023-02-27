namespace Fly.Core.Services;

public interface IScheduleService<T>
{
    public void Schedule(T t);

    public Task Start(int id);

    public Task Stop(int id);
}
