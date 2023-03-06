namespace Fly.Core.Services;

public interface IEmailSender<T>
{
    Task Send(T message);
}
