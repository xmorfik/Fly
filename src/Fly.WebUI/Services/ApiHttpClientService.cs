using Fly.WebUI.Interfaces;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

namespace Fly.WebUI.Services;

public class ApiHttpClientService : IApiHttpClientService
{
    private readonly ILogger<ApiHttpClientService> _logger;
    private readonly IHttpClientFactory _factory;
    private readonly ApiConfiguration _apiConfiguration;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ApiHttpClientService(
        ILogger<ApiHttpClientService> logger,
        IHttpClientFactory factory,
        IOptions<ApiConfiguration> apiConfiguration,
        IHttpContextAccessor httpContextAccessor)
    {
        _logger = logger;
        _factory = factory;
        _apiConfiguration = apiConfiguration.Value;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<HttpClient> GetClientAsync()
    {
        var accessToken = await _httpContextAccessor.HttpContext.GetTokenAsync("access_token");
        var client = _factory.CreateClient();
        client.BaseAddress = new Uri(_apiConfiguration.Uri + _apiConfiguration.Part);
        client.SetBearerToken(accessToken);
        return client;
    }
}
