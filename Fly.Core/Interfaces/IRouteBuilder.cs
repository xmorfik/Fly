namespace Fly.Core.Interfaces;

public interface IRouteBuilder<T, TResult>
{
    public TResult GetLocation(T t);
}
