

using Fly.Core.Entities;
using Fly.Core.Pagination;
using Fly.Core.Parameters;
using Fly.Core.Services;
using Fly.WebUI.Interfaces;
using Newtonsoft.Json;
using System.Text;

namespace Fly.WebUI.Services;

public class ManagerRequestService : IService<Manager, ManagerParameter>
{
    private readonly ILogger<ManagerRequestService> _logger;
    private readonly IApiHttpClientService _httpClientService;
    private readonly IParametersParser _parser;

    public ManagerRequestService(
        ILogger<ManagerRequestService> logger,
        IApiHttpClientService httpClientService,
        IParametersParser parser)
    {
        _logger = logger;
        _httpClientService = httpClientService;
        _parser = parser;
    }

    public async Task CreateAsync(Manager item)
    {
        try
        {
            var itemJson = JsonConvert.SerializeObject(item);
            var content = new StringContent(itemJson, Encoding.UTF8, "application/json");
            var client = await _httpClientService.GetClientAsync();
            var response = await client.PostAsync("managers", content);
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError(response.ReasonPhrase);
            }
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
            var response = await client.DeleteAsync($"managers/{id}");
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError(response.ReasonPhrase);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }

    public async Task<Response<Manager>> GetAsync(int id)
    {
        try
        {
            var client = await _httpClientService.GetClientAsync();
            var response = await client.GetAsync($"managers/{id}");
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError(response.ReasonPhrase);
                return new Response<Manager>(new Manager()) { Succeeded = false };
            }

            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Response<Manager>>(responseString);

            if (result == null)
            {
                return new Response<Manager>(new Manager()) { Succeeded = false };
            }

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }

    public async Task<PagedResponse<Manager>> GetListAsync(ManagerParameter parameter, Page page)
    {

        var paramsStr = _parser.Parse(parameter, page);
        var client = await _httpClientService.GetClientAsync();
        try
        {
            var response = await client.GetAsync("managers?" + paramsStr);
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError(response.ReasonPhrase);
                return new PagedResponse<Manager>(new List<Manager>(), new MetaData());
            }
            var responseString = await response.Content.ReadAsStringAsync();
            var headerValues = response.Headers.GetValues("X-Pagination");
            var jsonMetaData = headerValues.FirstOrDefault();
            var items = JsonConvert.DeserializeObject<List<Manager>>(responseString);
            var metaData = JsonConvert.DeserializeObject<MetaData>(jsonMetaData);
            var pagedResponse = new PagedResponse<Manager>(items, metaData);
            return pagedResponse;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }

    public async Task UpdateAsync(Manager item)
    {
        try
        {
            var client = await _httpClientService.GetClientAsync();
            var itemJson = JsonConvert.SerializeObject(item);
            var content = new StringContent(itemJson, Encoding.UTF8, "application/json");
            var response = await client.PutAsync("managers", content);
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError(response.ReasonPhrase);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }
}
