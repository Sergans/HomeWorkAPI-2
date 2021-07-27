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
            //var fromParameter = request.FromTime.ToString();
            //var toParameter = request.ToTime.ToString();

            //var httpRequest = new HttpRequestMessage(HttpMethod.Get,$"{request.ClientBaseAddress}/{fromParameter}/to/{toParameter}");
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"{request.ClientBaseAddress}");
            httpRequest.Headers.Add("Accept", "application/json;charset=utf-8");
            try
            {
                HttpResponseMessage response = _httpClient.SendAsync(httpRequest).Result;
                var str = response.StatusCode;
                
                using var responseStream = response.Content.ReadAsStreamAsync().Result;
                //StreamReader streamReader = new StreamReader(responseStream);
                var stream= JsonSerializer.DeserializeAsync<AllCpuMetricsApiResponse>(responseStream).Result;
                
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
