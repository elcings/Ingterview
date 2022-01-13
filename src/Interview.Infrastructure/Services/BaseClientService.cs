using Interview.Application.Common.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Polly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Interview.Infrastructure.Services
{
    public abstract class BaseClientService
    {
        private HttpClient _httpClient;
        private readonly ILogger<ExternalClientService> _logger;
        private ExternalSettings _setting;
        public BaseClientService(HttpClient httpClient,   ILogger<ExternalClientService> logger, ExternalSettings setting)
        {
            _httpClient = httpClient;
            _logger = logger;
            _setting = setting; ;
        }

        public async Task<HttpResponseMessage> MakeRequest(HttpMethod method, string uri)
        {
            HttpResponseMessage response;
            var msg = new HttpRequestMessage
            {
                Method = method,
                RequestUri = new Uri(_setting.BaseAddress + uri)
            };

            //msg.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //msg.Headers.Authorization=  new AuthenticationHeaderValue("Bearer", $"{_setting.Token}");

            var bodyContent = GetBodyContent();
            if (bodyContent != null)
                msg.Content = new StringContent(JsonConvert.SerializeObject(bodyContent));

            try
            {
                response = await _httpClient.SendAsync(msg);
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Request:  ExternalClientService.GetFuelLevelAsync");
                throw;
            }
            catch (TaskCanceledException ex)
            {
                _logger.LogError(ex, "Request:  ExternalClientService.GetFuelLevelAsync");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Request:  ExternalClientService.GetFuelLevelAsync");
                throw;
            }

            return response;
        }
        protected virtual object GetBodyContent()
        {
            return default;
        }
        
    }
}
