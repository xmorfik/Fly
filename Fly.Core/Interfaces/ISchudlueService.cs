namespace Fly.Core.Interfaces;

public interface IScheduleService<T>
{
    public void Schedule(T t);

    public Task Start(int id);

    public Task Stop(int id);
}
