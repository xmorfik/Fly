using Cysharp.Web;
using Fly.Core.Entities;
using Fly.Core.Pagination;
using Fly.Core.Parameters;
using Fly.Core.Services;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Text;

namespace Fly.WebUI.Services
{
    public class AircraftRequestService : IService<Aircraft, AircraftParameter>
    {
        private readonly ILogger<AircraftRequestService> _logger;
        private readonly IHttpClientFactory _factory;
        private readonly ApiConfiguration _apiConfiguration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AircraftRequestService(
            ILogger<AircraftRequestService> logger,
            IHttpClientFactory factory,
            IOptions<ApiConfiguration> apiConfiguration,
            IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _apiConfiguration = apiConfiguration.Value;
            _factory = factory;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task CreateAsync(Aircraft item)
        {
            try
            {
                var itemJson = JsonConvert.SerializeObject(item);
                var content = new StringContent(itemJson, Encoding.UTF8, "application/json");
                var accessToken = await _httpContextAccessor.HttpContext.GetTokenAsync("access_token");
                var client = _factory.CreateClient();
                client.BaseAddress = new Uri(_apiConfiguration.Uri + _apiConfiguration.Part);
                client.SetBearerToken(accessToken);
                await client.PostAsync("aircrafts", content);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                var accessToken = await _httpContextAccessor.HttpContext.GetTokenAsync("access_token");
                var client = _factory.CreateClient();
                client.BaseAddress = new Uri(_apiConfiguration.Uri + _apiConfiguration.Part);
                client.SetBearerToken(accessToken);
                await client.DeleteAsync($"aircrafts/{id}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<Response<Aircraft>> GetAsync(int id)
        {
            try
            {
                var accessToken = await _httpContextAccessor.HttpContext.GetTokenAsync("access_token");
                var client = _factory.CreateClient();
                client.BaseAddress = new Uri(_apiConfiguration.Uri + _apiConfiguration.Part);
                client.SetBearerToken(accessToken);
                var response = await client.GetAsync($"aircrafts/{id}");
                var responseString = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<Response<Aircraft>>(responseString);

                if (result == null)
                {
                    return new Response<Aircraft>(new Aircraft()) { Succeeded = false };
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<PagedResponse<Aircraft>> GetListAsync(AircraftParameter parameter, Page page)
        {
            var queryParameters = WebSerializer.ToQueryString(parameter);
            var queryPage = WebSerializer.ToQueryString(page);
            var accessToken = await _httpContextAccessor.HttpContext.GetTokenAsync("access_token");
            var client = _factory.CreateClient();
            client.BaseAddress = new Uri(_apiConfiguration.Uri + _apiConfiguration.Part);
            client.SetBearerToken(accessToken);
            try
            {
                var response = await client.GetAsync("aircrafts?" + queryParameters.TrimStart('&') + '&' + queryPage);
                var responseString = await response.Content.ReadAsStringAsync();
                var headerValues = response.Headers.GetValues("X-Pagination");
                var jsonMetaData = headerValues.FirstOrDefault();
                var items = JsonConvert.DeserializeObject<List<Aircraft>>(responseString);
                var metaData = JsonConvert.DeserializeObject<MetaData>(jsonMetaData);
                var pagedResponse = new PagedResponse<Aircraft>(items, metaData);
                return pagedResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task UpdateAsync(Aircraft item)
        {
            try
            {
                var accessToken = await _httpContextAccessor.HttpContext.GetTokenAsync("access_token");
                var client = _factory.CreateClient();
                client.BaseAddress = new Uri(_apiConfiguration.Uri + _apiConfiguration.Part);
                client.SetBearerToken(accessToken);
                var itemJson = JsonConvert.SerializeObject(item);
                var content = new StringContent(itemJson, Encoding.UTF8, "application/json");
                await client.PutAsync("aircrafts", content);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
    }
}
