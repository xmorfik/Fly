using Microsoft.Extensions.Options;

namespace Fly.WebUI.Services;

public class ApiUriService
{
    public HttpClient HttpClient { get; init; }
    private readonly ApiConfiguration _apiConfiguration;

    public ApiUriService(IOptions<ApiConfiguration> apiConfiguration, HttpClient httpClient)
    {
        _apiConfiguration = apiConfiguration.Value;
        HttpClient = httpClient;
        HttpClient.BaseAddress = new Uri(_apiConfiguration.Uri + _apiConfiguration.Part);
    }
}
