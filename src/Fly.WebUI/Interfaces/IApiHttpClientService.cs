namespace Fly.WebUI.Interfaces;

public interface IApiHttpClientService
{
    Task<HttpClient> GetClientAsync();
}
