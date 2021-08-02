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
    }
    public class MetricsAgentClient : IMetricsAgentClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger _logger;
        public MetricsAgentClient(HttpClient httpClient, ILogger logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public AllCpuMetricsApiResponse GetAllCpuMetrics(GetAllCpuMetricsApiRequest request)
        {
            //var fromParameter = request.FromTime.ToUnixTimeSeconds();
            //var toParameter = request.ToTime.ToUnixTimeSeconds();
            var fromParameter = request.FromTime;
            var toParameter = request.ToTime;

           var httpRequest = new HttpRequestMessage(HttpMethod.Get,$"{request.ClientBaseAddress}/{fromParameter}/to/{toParameter}");
           // var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"{request.ClientBaseAddress= "http://localhost:5010/api/metrics/cpu/from/2021-07-30Z14:00:30/to/2021-07-30Z14:00:45"}");
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
                _logger.LogError(ex.Message);
            }
            return null;

        }
    }
}
