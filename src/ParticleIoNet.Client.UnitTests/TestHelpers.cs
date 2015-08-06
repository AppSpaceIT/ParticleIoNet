using System;
using System.Net.Http;
using System.Web.Http;

namespace ParticleIoNet.Client.UnitTests
{
    internal static class TestHelpers
    {
        public static HttpClient GetHttpClient<T>(T value)
        {
            var handler = new FakeDelegatingHandler<T>(value);
            var server = new HttpServer(new HttpConfiguration(), handler);
            var httpClient = new HttpClient(server)
            {
                BaseAddress = new Uri("http://localhost/api")
            };

            return httpClient;
        }
    }
}
