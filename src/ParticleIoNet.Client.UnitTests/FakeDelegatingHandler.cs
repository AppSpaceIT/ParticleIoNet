using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace ParticleIoNet.Client.UnitTests
{
    public class FakeDelegatingHandler<T> : DelegatingHandler
    {
        private readonly Func<HttpRequestMessage, HttpResponseMessage> _httpResponseMessageFunc;

        public FakeDelegatingHandler(T value)
            : this(HttpStatusCode.OK, value)
        {
        }

        public FakeDelegatingHandler(HttpStatusCode statusCode)
            : this(statusCode, default(T))
        {
        }

        public FakeDelegatingHandler(HttpStatusCode statusCode, T value)
        {
            _httpResponseMessageFunc = request => request.CreateResponse(statusCode, value);
        }

        public FakeDelegatingHandler(
            Func<HttpRequestMessage, HttpResponseMessage> httpResponseMessageFunc)
        {
            _httpResponseMessageFunc = httpResponseMessageFunc;
        }

        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return Task.Factory.StartNew(() => _httpResponseMessageFunc(request), cancellationToken);
        }
    }
}