using Cysharp.Web;
using Fly.Core.Entities;
using Fly.Core.Pagination;
using Fly.Core.Parameters;
using Fly.Core.Services;
using Fly.WebUI.Services;
using Newtonsoft.Json;
using System.Text;

namespace Fly.WebUI.RequestServices
{
    public class AircraftRequestService : IService<Aircraft, AircraftParameter>
    {
        private readonly ApiUriService _apiUriService;
        private readonly ILogger<AircraftRequestService> _logger;

        public AircraftRequestService(ApiUriService apiUri, ILogger<AircraftRequestService> logger)
        {
            _apiUriService = apiUri;
            _logger = logger;
        }

        public async Task CreateAsync(Aircraft item)
        {
            try
            {
                var itemJson = JsonConvert.SerializeObject(item);
                var content = new StringContent(itemJson, Encoding.UTF8, "application/json");
                await _apiUriService.HttpClient.PostAsync("aircrafts", content);
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
                await _apiUriService.HttpClient.DeleteAsync($"aircrafts/{id}");
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
                var response = await _apiUriService.HttpClient.GetAsync($"aircrafts/{id}");
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

        public async Task<PagedResponse<ICollection<Aircraft>>> GetListAsync(AircraftParameter parameter, Page page)
        {
            try
            {
                var queryParameters = WebSerializer.ToQueryString(parameter);
                var queryPage = WebSerializer.ToQueryString(page);
                var response = await _apiUriService.HttpClient.GetAsync("aircrafts?" + queryParameters + queryPage);
                var responseString = await response.Content.ReadAsStringAsync();
                var items = JsonConvert.DeserializeObject<PagedResponse<ICollection<Aircraft>>>(responseString);
                return items;
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
                var itemJson = JsonConvert.SerializeObject(item);
                var content = new StringContent(itemJson, Encoding.UTF8, "application/json");
                await _apiUriService.HttpClient.PutAsync("aircrafts", content);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
    }
}
