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
using System.Text;
using System.Threading.Tasks;

namespace Interview.Infrastructure.Services
{
    public class ExternalClientService: IExternalClientService
    {
        private readonly ILogger<ExternalClientService> _logger;
        HttpClient httpClient;
        public ExternalClientService(ExternalSettings externalSettings, ILogger<ExternalClientService> logger,HttpClient httpClient)
        {
            _logger = logger;
            httpClient.BaseAddress = new Uri(externalSettings.BaseAddress);
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<string> GetErrorAsync()
        {
            string content = string.Empty;
            HttpResponseMessage response;
            var policy = Policy.Handle<Exception>().WaitAndRetryAsync(5, retryAttemp => TimeSpan.FromSeconds(Math.Pow(2, retryAttemp)), (ex, time) =>
              {
                  _logger.LogError(ex, "Request:  ExternalClientService.GetFuelLevelAsync");
              });

            
           await policy.ExecuteAsync( async () =>
          {
              response = await httpClient.GetAsync("/error");
              content = await  response.Content.ReadAsStringAsync();
              return Task.FromResult(content);
          });
            return content;
        }

        public async Task<int> GetFuelLevelAsync()
        {
            int noUnit = 0;
            try
            {
                HttpResponseMessage response = await httpClient.GetAsync("/fuel");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();

                    if (int.TryParse(content, out int result))
                    {
                        return result;
                    }
                    else
                    {
                        noUnit = int.Parse(content.RemoveUnit());
                    }

                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Request:  ExternalClientService.GetFuelLevelAsync");
               
            }

            return noUnit;
        }

        public async Task<long> GetTravelDistanceAsync()
        {
            long noUnit = 0;
            try
            {
                HttpResponseMessage response = await httpClient.GetAsync("/odometer");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var reading = Newtonsoft.Json.JsonConvert.DeserializeObject<OdometerReading>(content);
                    var mileage = reading.Total;
                   
                    if (long.TryParse(mileage, out long result))
                    {
                        return result;
                    }
                    else
                    {
                        noUnit = long.Parse(mileage.RemoveUnit());
                    }

                }
            }
            catch (Exception ex )
            {
                _logger.LogError(ex, "Request:  ExternalClientService.GetTravelDistanceAsync");
            }

            return noUnit;
        }
    }
}
