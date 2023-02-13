﻿using Cysharp.Web;
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
        private readonly ApiHttpClientService _httpClientService;

        public AirportRequestService(
            ILogger<AirportRequestService> logger,
            ApiHttpClientService httpClientService)
        {
            _logger = logger;
            _httpClientService = httpClientService;
        }

        public async Task CreateAsync(Airport item)
        {
            try
            {
                var itemJson = JsonConvert.SerializeObject(item);
                var content = new StringContent(itemJson, Encoding.UTF8, "application/json");
                var client = await _httpClientService.GetClientAsync();
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
                var client = await _httpClientService.GetClientAsync();
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
                var client = await _httpClientService.GetClientAsync();
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
            var client = await _httpClientService.GetClientAsync();
            try
            {
                var response = await client.GetAsync("Airports?" + queryParameters.TrimStart('&') + '&' + queryPage);
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
                var client = await _httpClientService.GetClientAsync();
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