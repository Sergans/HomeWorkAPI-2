using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFManager.Request;
using WPFManager.Response;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace WPFManager.Client
{
   public interface IClient
    {
        public WPFResponse GetCpuMetric(WPFRequest request);
        public WPFResponse GetDotNetMetric(WPFRequest request);
        public WPFResponse GetNetWorkMetric(WPFRequest request);
        public WPFResponse GetRamMetric(WPFRequest request);
        public WPFResponse GetHddMetric(WPFRequest request);
    }
    public class WPFClient : IClient
    {
        private readonly HttpClient _httpClient;
        public WPFClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public WPFResponse GetCpuMetric(WPFRequest request)
        {
            var fromParameter = request.FromTime;
            var toParameter = request.ToTime;

            var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"{request.ClientBaseAddress}/api/metrics/cpu/from/{fromParameter}/to/{toParameter}");

            httpRequest.Headers.Add("Accept", "application/json");
            try
            {
                HttpResponseMessage response = _httpClient.SendAsync(httpRequest).Result;

                using var responseStream = response.Content.ReadAsStreamAsync().Result;
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var stream = JsonSerializer.DeserializeAsync<WPFResponse>(responseStream, options).Result;

                return stream;
            }
            catch (Exception ex)
            {
                // _logger.LogError(ex.Message);
            }
            return null;
        }

        public WPFResponse GetDotNetMetric(WPFRequest request)
        {
            var fromParameter = request.FromTime;
            var toParameter = request.ToTime;

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
                var stream = JsonSerializer.DeserializeAsync<WPFResponse>(responseStream, options).Result;

                return stream;
            }
            catch (Exception ex)
            {
                // _logger.LogError(ex.Message);
            }
            return null;
        }

        public WPFResponse GetHddMetric(WPFRequest request)
        {
            var fromParameter = request.FromTime;
            var toParameter = request.ToTime;

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
                var stream = JsonSerializer.DeserializeAsync<WPFResponse>(responseStream, options).Result;

                return stream;
            }
            catch (Exception ex)
            {
                // _logger.LogError(ex.Message);
            }
            return null;
        }

        public WPFResponse GetNetWorkMetric(WPFRequest request)
        {
            var fromParameter = request.FromTime;
            var toParameter = request.ToTime;

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
                var stream = JsonSerializer.DeserializeAsync<WPFResponse>(responseStream, options).Result;

                return stream;
            }
            catch (Exception ex)
            {
                // _logger.LogError(ex.Message);
            }
            return null;
        }

        public WPFResponse GetRamMetric(WPFRequest request)
        {
            var fromParameter = request.FromTime;
            var toParameter = request.ToTime;

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
                var stream = JsonSerializer.DeserializeAsync<WPFResponse>(responseStream, options).Result;

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

