using Cysharp.Web;
using Fly.Core.Entities;
using Fly.Core.Pagination;
using Fly.Core.Parameters;
using Fly.Core.Services;
using Newtonsoft.Json;
using System.Text;

namespace Fly.WebUI.Services
{
    public class AircraftRequestService : IService<Aircraft, AircraftParameter>
    {
        private readonly ILogger<AircraftRequestService> _logger;
        private readonly ApiHttpClientService _httpClientService;

        public AircraftRequestService(
            ILogger<AircraftRequestService> logger,
            ApiHttpClientService httpClientService)
        {
            _logger = logger;
            _httpClientService = httpClientService;
        }

        public async Task CreateAsync(Aircraft item)
        {
            try
            {
                var itemJson = JsonConvert.SerializeObject(item);
                var content = new StringContent(itemJson, Encoding.UTF8, "application/json");
                var client = await _httpClientService.GetClientAsync();
                await client.PostAsync("Aircrafts", content);
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
                var client = await _httpClientService.GetClientAsync();
                await client.DeleteAsync($"Aircrafts/{id}");
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
                var client = await _httpClientService.GetClientAsync();
                var response = await client.GetAsync($"Aircrafts/{id}");
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
            var client = await _httpClientService.GetClientAsync();
            try
            {
                var response = await client.GetAsync("Aircrafts?" + queryParameters.TrimStart('&') + '&' + queryPage);
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
                var client = await _httpClientService.GetClientAsync();
                var itemJson = JsonConvert.SerializeObject(item);
                var content = new StringContent(itemJson, Encoding.UTF8, "application/json");
                await client.PutAsync("Aircrafts", content);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
    }
}
