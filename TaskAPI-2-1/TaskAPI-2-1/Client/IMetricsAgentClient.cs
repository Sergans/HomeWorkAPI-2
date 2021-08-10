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
        AllDotNetMetricsApiResponse GetAllDotNetMetrics(GetAllDotNetMetricsApiRequest request);
        AllNetWorkMetricsApiResponse GetAllNetWorkMetrics(GetAllNetWorkMetricsApiRequest request);

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
            var fromParameter = request.FromTime.ToString("O");
            var toParameter = request.ToTime.ToString("O");
           
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
            var fromParameter = request.FromTime.ToString("O");
            var toParameter = request.ToTime.ToString("O");
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"{request.ClientBaseAddress}/api/metrics/hdd/left/from/{fromParameter}/to/{toParameter}");
            httpRequest.Headers.Add("Accept", "application/json");
            try
            {
                HttpResponseMessage response = _httpClient.SendAsync(httpRequest).Result;

                using var responseStream = response.Content.ReadAsStreamAsync().Result;
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var stream = JsonSerializer.DeserializeAsync<AllHddMetricsApiResponse>(responseStream, options).Result;

                return stream;
            }
            catch (Exception ex)
            {
                // _logger.LogError(ex.Message);
            }
            return null;
        }

        public AllRamMetricsApiResponse GetAllRamMetrics(GetAllRamMetricsApiRequest request)
        {
            var fromParameter = request.FromTime.ToString("O");
            var toParameter = request.ToTime.ToString("O");
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"{request.ClientBaseAddress}/api/metrics/ram/available/from/{fromParameter}/to/{toParameter}");
            httpRequest.Headers.Add("Accept", "application/json");
            try
            {
                HttpResponseMessage response = _httpClient.SendAsync(httpRequest).Result;

                using var responseStream = response.Content.ReadAsStreamAsync().Result;
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var stream = JsonSerializer.DeserializeAsync<AllRamMetricsApiResponse>(responseStream, options).Result;

                return stream;
            }
            catch (Exception ex)
            {
                // _logger.LogError(ex.Message);
            }
            return null;
        }

        public AllDotNetMetricsApiResponse GetAllDotNetMetrics(GetAllDotNetMetricsApiRequest request)
        {
            var fromParameter = request.FromTime.ToString("O");
            var toParameter = request.ToTime.ToString("O");
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"{request.ClientBaseAddress}/api/metrics/dotnet/errors-count/from/{fromParameter}/to/{toParameter}");
            httpRequest.Headers.Add("Accept", "application/json");
            try
            {
                HttpResponseMessage response = _httpClient.SendAsync(httpRequest).Result;

                using var responseStream = response.Content.ReadAsStreamAsync().Result;
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var stream = JsonSerializer.DeserializeAsync<AllDotNetMetricsApiResponse>(responseStream, options).Result;

                return stream;
            }
            catch (Exception ex)
            {
                // _logger.LogError(ex.Message);
            }
            return null;
        }

        public AllNetWorkMetricsApiResponse GetAllNetWorkMetrics(GetAllNetWorkMetricsApiRequest request)
        {
            var fromParameter = request.FromTime.ToString("O");
            var toParameter = request.ToTime.ToString("O");
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"{request.ClientBaseAddress}/api/metrics/network/from/{fromParameter}/to/{toParameter}");
            httpRequest.Headers.Add("Accept", "application/json");
            try
            {
                HttpResponseMessage response = _httpClient.SendAsync(httpRequest).Result;

                using var responseStream = response.Content.ReadAsStreamAsync().Result;
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var stream = JsonSerializer.DeserializeAsync<AllNetWorkMetricsApiResponse>(responseStream, options).Result;

                return stream;
            }
            catch (Exception ex)
            {
                // _logger.LogError(ex.Message);
            }
            return null;
        }
    }
}
