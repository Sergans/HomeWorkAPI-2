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
    class WPFClient
    {
        private readonly HttpClient _httpClient;
        
        public WPFClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
           
        }
        public WPFResponse GetMetric(WPFRequest request)
        {
            var fromParameter = request.FromTime.ToString("O");
            var toParameter = request.ToTime.ToString("O");

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
    }
}
