namespace Fly.Core.Services;

public interface IEmailSender<T>
{
    void Send(T message);
}
