using Cysharp.Web;
using Fly.Core.Entities;
using Fly.Core.Pagination;
using Fly.Core.Parameters;
using Fly.Core.Services;
using Newtonsoft.Json;
using System.Text;

namespace Fly.WebUI.Services
{
    public class FlightRequestService : IService<Flight, FlightParameter>
    {
        private readonly ILogger<FlightRequestService> _logger;
        private readonly ApiHttpClientService _httpClientService;

        public FlightRequestService(
            ILogger<FlightRequestService> logger,
            ApiHttpClientService httpClientService)
        {
            _logger = logger;
            _httpClientService = httpClientService;
        }

        public async Task CreateAsync(Flight item)
        {
            try
            {
                var itemJson = JsonConvert.SerializeObject(item);
                var content = new StringContent(itemJson, Encoding.UTF8, "application/json");
                var client = await _httpClientService.GetClientAsync();
                await client.PostAsync("Flights", content);
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
                await client.DeleteAsync($"Flights/{id}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<Response<Flight>> GetAsync(int id)
        {
            try
            {
                var client = await _httpClientService.GetClientAsync();
                var response = await client.GetAsync($"Flights/{id}");
                var responseString = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<Response<Flight>>(responseString);

                if (result == null)
                {
                    return new Response<Flight>(new Flight()) { Succeeded = false };
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<PagedResponse<Flight>> GetListAsync(FlightParameter parameter, Page page)
        {
            var queryParameters = WebSerializer.ToQueryString(parameter);
            var queryPage = WebSerializer.ToQueryString(page);
            var client = await _httpClientService.GetClientAsync();
            try
            {
                var response = await client.GetAsync("flights?" + queryParameters.TrimStart('&') + '&' + queryPage);
                var responseString = await response.Content.ReadAsStringAsync();
                var headerValues = response.Headers.GetValues("X-Pagination");
                var jsonMetaData = headerValues.FirstOrDefault();
                var items = JsonConvert.DeserializeObject<List<Flight>>(responseString);
                var metaData = JsonConvert.DeserializeObject<MetaData>(jsonMetaData);
                var pagedResponse = new PagedResponse<Flight>(items, metaData);
                return pagedResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task UpdateAsync(Flight item)
        {
            try
            {
                var client = await _httpClientService.GetClientAsync();
                var itemJson = JsonConvert.SerializeObject(item);
                var content = new StringContent(itemJson, Encoding.UTF8, "application/json");
                await client.PutAsync("Flights", content);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
    }
}
