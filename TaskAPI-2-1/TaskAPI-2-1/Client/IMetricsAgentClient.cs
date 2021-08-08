using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskAPI_2_1.Request;
using TaskAPI_2_1.Responses;
using System.Net.Http;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;
using TaskAPI_2_1.Agents.Model;
using System.IO;

namespace TaskAPI_2_1.Client
{
   public interface IMetricsAgentClient
    {
        AllCpuMetricsApiResponse GetAllCpuMetrics(GetAllCpuMetricsApiRequest request);
        AllRamMetricsApiResponse GetAllRamMetrics(GetAllRamMetricsApiRequest request);
        AllHddMetricsApiResponse GetAllHddMetrics(GetAllHddMetricsApiRequest request);
        AllDotNetMetricsApiResponse GetDonNetMetrics(GetAllDotNetMetricsApiRequest request);
        AllNetWorkMetricsApiResponse GetDonNetMetrics(GetAllNetWorkMetricsApiRequest request);

    }
    public class MetricsAgentClient : IMetricsAgentClient
    {
        private readonly HttpClient _httpClient;
        //private readonly ILogger _logger;
        public MetricsAgentClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
           // _logger = logger;
        }

        public AllCpuMetricsApiResponse GetAllCpuMetrics(GetAllCpuMetricsApiRequest request)
        {
            var fromParameter = request.FromTime.ToUniversalTime();
            var toParameter = request.ToTime.AddDays(1);
           
           var httpRequest = new HttpRequestMessage(HttpMethod.Get,$"{request.ClientBaseAddress}/api/metrics/cpu/from/{fromParameter}/to/{toParameter}");
           
            httpRequest.Headers.Add("Accept", "application/json");
            try
            {
                HttpResponseMessage response = _httpClient.SendAsync(httpRequest).Result;
               
                using var responseStream = response.Content.ReadAsStreamAsync().Result;
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var stream= JsonSerializer.DeserializeAsync<AllCpuMetricsApiResponse>(responseStream,options).Result;
                
                return stream;
            }
            catch (Exception ex)
            {
               // _logger.LogError(ex.Message);
            }
            return null;

        }

        public AllHddMetricsApiResponse GetAllHddMetrics(GetAllHddMetricsApiRequest request)
        {
            throw new NotImplementedException();
        }

        public AllRamMetricsApiResponse GetAllRamMetrics(GetAllRamMetricsApiRequest request)
        {
            throw new NotImplementedException();
        }

        public AllDotNetMetricsApiResponse GetDonNetMetrics(GetAllDotNetMetricsApiRequest request)
        {
            throw new NotImplementedException();
        }

        public AllNetWorkMetricsApiResponse GetDonNetMetrics(GetAllNetWorkMetricsApiRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
