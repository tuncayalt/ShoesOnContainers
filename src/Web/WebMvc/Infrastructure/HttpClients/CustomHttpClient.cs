using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ShoesOnContainers.Web.WebMvc.Infrastructure.HttpClients
{
    public class CustomHttpClient : IHttpClient
    {
        private readonly ILogger<CustomHttpClient> _logger;
        private HttpClient _client;

        public CustomHttpClient(ILogger<CustomHttpClient> logger)
        {
            _client = new HttpClient();
            _logger = logger;
        }

        public async Task<string> GetStringAsync(string uri)
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            var response = await _client.SendAsync(requestMessage);

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<HttpResponseMessage> PostAsync<T>(string uri, T item)
        {
            return await PostOrPutAsync(uri, item, HttpMethod.Post);
        }

        public async Task<HttpResponseMessage> PutAsync<T>(string uri, T item)
        {
            return await PostOrPutAsync(uri, item, HttpMethod.Put);
        }

        private async Task<HttpResponseMessage> PostOrPutAsync<T>(string uri, T item, HttpMethod method)
        {
            if (method != HttpMethod.Post && method != HttpMethod.Put)
            {
                throw new ArgumentException("Method must be Post or Put", nameof(method));
            }

            var requestMessage = new HttpRequestMessage(method, uri)
            {
                Content = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json")
            };

            var responseMessage = await _client.SendAsync(requestMessage);

            // raise exception if 500
            // needed for circuit breakers
            if (responseMessage.StatusCode == HttpStatusCode.InternalServerError)
            {
                throw new HttpRequestException();
            }

            return responseMessage;
        }

        public async Task<HttpResponseMessage> DeleteAsync<T>(string uri)
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            return await _client.SendAsync(requestMessage);
        }
    }
}
