namespace Fly.Core.Services;

public interface ITicketsGeneratorService<T>
{
    public Task Generate(T t);
}
