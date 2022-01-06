using Interview.Application.Common;
using Interview.Application.Common.Interfaces;
using Interview.Application.Common.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
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
    public class ExternalClientService: IExternalClientService
    {
        private readonly ILogger<ExternalClientService> _logger;
        HttpClient _httpClient;
        public ExternalClientService(ILogger<ExternalClientService> logger,HttpClient httpClient)
        {
            _logger = logger;
            _httpClient = httpClient;
        }

        public async Task<DinResponse> GetDinOfficesAsync()
        {
             string content = string.Empty;
            HttpResponseMessage response;
            var policy = Policy.Handle<Exception>().WaitAndRetryAsync(2, retryAttemp => TimeSpan.FromSeconds(Math.Pow(2, retryAttemp)), (ex, time) =>
              {
                  _logger.LogError(ex.Message, "Request:  ExternalClientService.GetFuelLevelAsync");
              });

            
           await policy.ExecuteAsync( async () =>
          {
              response = await _httpClient.GetAsync("getDinOffices");
              content = await  response.Content.ReadAsStringAsync();
          });
            var result = JsonConvert.DeserializeObject<DinResponse>(content);
            return result;
        }

      
    }
}
