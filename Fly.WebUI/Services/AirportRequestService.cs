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
    public class AirportRequestService : IService<Airport, AirportParameter>
    {
        private readonly ILogger<AirportRequestService> _logger;
        private readonly IHttpClientFactory _factory;
        private readonly ApiConfiguration _apiConfiguration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AirportRequestService(
            ILogger<AirportRequestService> logger,
            IHttpClientFactory factory,
            IOptions<ApiConfiguration> apiConfiguration,
            IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _apiConfiguration = apiConfiguration.Value;
            _factory = factory;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task CreateAsync(Airport item)
        {
            try
            {
                var itemJson = JsonConvert.SerializeObject(item);
                var content = new StringContent(itemJson, Encoding.UTF8, "application/json");
                var accessToken = await _httpContextAccessor.HttpContext.GetTokenAsync("access_token");
                var client = _factory.CreateClient();
                client.BaseAddress = new Uri(_apiConfiguration.Uri + _apiConfiguration.Part);
                client.SetBearerToken(accessToken);
                await client.PostAsync("Airports", content);
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
                await client.DeleteAsync($"Airports/{id}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<Response<Airport>> GetAsync(int id)
        {
            try
            {
                var accessToken = await _httpContextAccessor.HttpContext.GetTokenAsync("access_token");
                var client = _factory.CreateClient();
                client.BaseAddress = new Uri(_apiConfiguration.Uri + _apiConfiguration.Part);
                client.SetBearerToken(accessToken);
                var response = await client.GetAsync($"Airports/{id}");
                var responseString = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<Response<Airport>>(responseString);

                if (result == null)
                {
                    return new Response<Airport>(new Airport()) { Succeeded = false };
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<PagedResponse<Airport>> GetListAsync(AirportParameter parameter, Page page)
        {
            var queryParameters = WebSerializer.ToQueryString(parameter);
            var queryPage = WebSerializer.ToQueryString(page);
            var accessToken = await _httpContextAccessor.HttpContext.GetTokenAsync("access_token");
            var client = _factory.CreateClient();
            client.BaseAddress = new Uri(_apiConfiguration.Uri + _apiConfiguration.Part);
            client.SetBearerToken(accessToken);
            try
            {
                var response = await client.GetAsync("airports?" + queryParameters.TrimStart('&') + '&' + queryPage);
                var responseString = await response.Content.ReadAsStringAsync();
                var headerValues = response.Headers.GetValues("X-Pagination");
                var jsonMetaData = headerValues.FirstOrDefault();
                var items = JsonConvert.DeserializeObject<List<Airport>>(responseString);
                var metaData = JsonConvert.DeserializeObject<MetaData>(jsonMetaData);
                var pagedResponse = new PagedResponse<Airport>(items, metaData);
                return pagedResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task UpdateAsync(Airport item)
        {
            try
            {
                var accessToken = await _httpContextAccessor.HttpContext.GetTokenAsync("access_token");
                var client = _factory.CreateClient();
                client.BaseAddress = new Uri(_apiConfiguration.Uri + _apiConfiguration.Part);
                client.SetBearerToken(accessToken);
                var itemJson = JsonConvert.SerializeObject(item);
                var content = new StringContent(itemJson, Encoding.UTF8, "application/json");
                await client.PutAsync("Airports", content);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
    }
}
