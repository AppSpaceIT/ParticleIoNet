using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace ParticleIoNet.Client
{
    internal class HttpClientHelper
    {
        private readonly HttpClient _httpClient;

        public HttpClientHelper(string token, HttpClient httpClient = null)
        {
            if (string.IsNullOrEmpty(token))
            {
                throw new ArgumentNullException("token");
            }

            Token = token;
            _httpClient = httpClient;
        }

        public string Token { get; set; }

        public HttpClient GetClient()
        {
            var client = _httpClient ?? new HttpClient
            {
                BaseAddress = new Uri("https://api.particle.io")
            };

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return client;
        }

        public HttpClient GetAuthorizedClient()
        {
            var client = GetClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);

            return client;
        }
    }
}