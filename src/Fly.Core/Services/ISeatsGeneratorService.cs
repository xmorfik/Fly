namespace Fly.Core.Services;

public interface ISeatsGeneratorService<T>
{
    public Task Generate(T t);
}
