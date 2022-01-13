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
    public class ExternalClientService: BaseClientService, IExternalClientService
    {

        public ExternalClientService(HttpClient httpClient,ILogger<ExternalClientService> logger, ExternalSettings setting) 
            :base(httpClient,logger,setting)
        {

        }

        public async Task<DinResponse> GetDinOfficesAsync()
        {
            var response = await base.MakeRequest(HttpMethod.Get, "getDinOffices");
           
            var  content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<DinResponse>(content);
            return result;
        }

      
    }
}
