namespace Fly.Core.Services;

public interface IRouteBuilder<T, TResult>
{
    public TResult GetLocation(T t);
}
