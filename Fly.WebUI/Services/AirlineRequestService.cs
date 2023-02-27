

using Fly.Core.Entities;
using Fly.Core.Pagination;
using Fly.Core.Parameters;
using Fly.Core.Services;
using Fly.WebUI.Interfaces;
using Newtonsoft.Json;
using System.Text;

namespace Fly.WebUI.Services;

public class AirlineRequestService : IService<Airline, AirlineParameter>
{
    private readonly ILogger<AirlineRequestService> _logger;
    private readonly IApiHttpClientService _httpClientService;
    private readonly IParametersParser _parser;

    public AirlineRequestService(
        ILogger<AirlineRequestService> logger,
        IApiHttpClientService httpClientService,
        IParametersParser parser)
    {
        _logger = logger;
        _httpClientService = httpClientService;
        _parser = parser;
    }

    public async Task CreateAsync(Airline item)
    {
        try
        {
            var itemJson = JsonConvert.SerializeObject(item);
            var content = new StringContent(itemJson, Encoding.UTF8, "application/json");
            var client = await _httpClientService.GetClientAsync();
            var response = await client.PostAsync("Airlines", content);
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
            var response =  await client.DeleteAsync($"Airlines/{id}");
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

    public async Task<Response<Airline>> GetAsync(int id)
    {
        try
        {
            var client = await _httpClientService.GetClientAsync();
            var response = await client.GetAsync($"airlines/{id}");
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError(response.ReasonPhrase);
                return new Response<Airline>(new Airline()) { Succeeded = false };
            }

            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Response<Airline>>(responseString);
            if (result == null)
            {
                return new Response<Airline>(new Airline()) { Succeeded = false };
            }

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }

    public async Task<PagedResponse<Airline>> GetListAsync(AirlineParameter parameter, Page page)
    {

        var paramsStr = _parser.Parse(parameter, page);
        var client = await _httpClientService.GetClientAsync();
        try
        {
            var response = await client.GetAsync("Airlines?" + paramsStr);
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError(response.ReasonPhrase);
                return new PagedResponse<Airline>(new List<Airline>(), new MetaData());
            }
            var responseString = await response.Content.ReadAsStringAsync();
            var headerValues = response.Headers.GetValues("X-Pagination");
            var jsonMetaData = headerValues.FirstOrDefault();
            var items = JsonConvert.DeserializeObject<List<Airline>>(responseString);
            var metaData = JsonConvert.DeserializeObject<MetaData>(jsonMetaData);
            var pagedResponse = new PagedResponse<Airline>(items, metaData);
            return pagedResponse;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }

    public async Task UpdateAsync(Airline item)
    {
        try
        {
            var client = await _httpClientService.GetClientAsync();
            var itemJson = JsonConvert.SerializeObject(item);
            var content = new StringContent(itemJson, Encoding.UTF8, "application/json");
            var response = await client.PutAsync("Airlines", content);
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
