

using Fly.Core.Entities;
using Fly.Core.Pagination;
using Fly.Core.Parameters;
using Fly.Core.Services;
using Fly.WebUI.Interfaces;
using Newtonsoft.Json;
using System.Text;

namespace Fly.WebUI.Services;

public class SeatRequestService : IService<Seat, SeatParameter>
{
    private readonly ILogger<SeatRequestService> _logger;
    private readonly IApiHttpClientService _httpClientService;
    private readonly IParametersParser _parser;

    public SeatRequestService(
        ILogger<SeatRequestService> logger,
        IApiHttpClientService httpClientService,
        IParametersParser parser)
    {
        _logger = logger;
        _httpClientService = httpClientService;
        _parser = parser;
    }

    public async Task CreateAsync(Seat item)
    {
        try
        {
            var itemJson = JsonConvert.SerializeObject(item);
            var content = new StringContent(itemJson, Encoding.UTF8, "application/json");
            var client = await _httpClientService.GetClientAsync();
            var response = await client.PostAsync("seats", content);
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
            var response = await client.DeleteAsync($"seats/{id}");
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

    public async Task<ResponseBase<Seat>> GetAsync(int id)
    {
        try
        {
            var client = await _httpClientService.GetClientAsync();
            var response = await client.GetAsync($"seats/{id}");
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError(response.ReasonPhrase);
                return new ResponseBase<Seat>(new Seat()) { Succeeded = false };
            }

            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ResponseBase<Seat>>(responseString);

            if (result == null)
            {
                return new ResponseBase<Seat>(new Seat()) { Succeeded = false };
            }

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }

    public async Task<PagedResponse<Seat>> GetListAsync(SeatParameter parameter, Page page)
    {

        var paramsStr = _parser.Parse(parameter, page);
        var client = await _httpClientService.GetClientAsync();
        try
        {
            var response = await client.GetAsync("seats?" + paramsStr);
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError(response.ReasonPhrase);
                return new PagedResponse<Seat>(new List<Seat>(), new MetaData());
            }
            var responseString = await response.Content.ReadAsStringAsync();
            var headerValues = response.Headers.GetValues("X-Pagination");
            var jsonMetaData = headerValues.FirstOrDefault();
            var items = JsonConvert.DeserializeObject<List<Seat>>(responseString);
            var metaData = JsonConvert.DeserializeObject<MetaData>(jsonMetaData);
            var pagedResponse = new PagedResponse<Seat>(items, metaData);
            return pagedResponse;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }

    public async Task UpdateAsync(Seat item)
    {
        try
        {
            var client = await _httpClientService.GetClientAsync();
            var itemJson = JsonConvert.SerializeObject(item);
            var content = new StringContent(itemJson, Encoding.UTF8, "application/json");
            var response = await client.PutAsync("seats", content);
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
