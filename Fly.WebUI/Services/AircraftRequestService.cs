using Fly.Core.Entities;
using Fly.Core.Pagination;
using Fly.Core.Parameters;
using Fly.Core.Services;
using Fly.WebUI.Interfaces;
using Newtonsoft.Json;
using System.Text;

namespace Fly.WebUI.Services;

public class AircraftRequestService : IService<Aircraft, AircraftParameter>
{
    private readonly ILogger<AircraftRequestService> _logger;
    private readonly IApiHttpClientService _httpClientService;
    private readonly IParametersParser _parser;

    public AircraftRequestService(
        ILogger<AircraftRequestService> logger,
        IApiHttpClientService httpClientService,
        IParametersParser parser)
    {
        _logger = logger;
        _httpClientService = httpClientService;
        _parser = parser;
    }

    public async Task CreateAsync(Aircraft item)
    {
        try
        {
            var itemJson = JsonConvert.SerializeObject(item);
            var content = new StringContent(itemJson, Encoding.UTF8, "application/json");
            var client = await _httpClientService.GetClientAsync();
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
            var client = await _httpClientService.GetClientAsync();
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
            var client = await _httpClientService.GetClientAsync();
            var response = await client.GetAsync($"aircrafts/{id}");
            if (response.IsSuccessStatusCode!)
            {
                _logger.LogError(response.ReasonPhrase);
                return new Response<Aircraft>(new Aircraft()) { Succeeded = false };
            }
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
        var parser = new ParametersParser();
        var str = parser.Parse(parameter, page);
        var client = await _httpClientService.GetClientAsync();
        try
        {
            var response = await client.GetAsync("aircrafts?" + str);
            if(response.IsSuccessStatusCode!)
            {
                _logger.LogError(response.ReasonPhrase);
                return new PagedResponse<Aircraft>(new List<Aircraft>(), new MetaData());
            }
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
            await client.PutAsync("aircrafts", content);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }
}
