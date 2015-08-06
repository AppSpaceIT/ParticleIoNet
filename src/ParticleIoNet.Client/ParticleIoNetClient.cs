using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ParticleIoNet.Client
{
    public class ParticleIoNetClient
    {
        #region ctor

        public ParticleIoNetClient(string token = null, HttpClient httpClient = null)
        {
            _httpClientHelper = new HttpClientHelper(token, httpClient);
        }

        #endregion

        #region private

        private readonly HttpClientHelper _httpClientHelper;
        private CancellationTokenSource _cancellationTokenSource;

        private static string Base64Auth(string username, string password)
        {
            return Convert.ToBase64String(Encoding.ASCII.GetBytes(string.Format("{0}:{1}", username, password)));
        }

        #endregion

        #region public

        public async Task<string> LoginAsync(string username, string password)
        {
            if (string.IsNullOrEmpty(username))
            {
                throw new ArgumentNullException("username");
            }

            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentNullException("password");
            }

            using (var client = _httpClientHelper.GetClient())
            {
                var formValues = new Dictionary<string, string>
                {
                    {"grant_type", "password"},
                    {"username", username},
                    {"password", password}
                }.ToList();

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                    Base64Auth("particle", "particle"));

                using (var response = await client.PostAsync("/oauth/token", new FormUrlEncodedContent(formValues)))
                {
                    response.EnsureSuccessStatusCode();

                    var token = await response.Content.ReadAsAsync<CreateToken>();

                    _httpClientHelper.Token = token.AccessToken;

                    return token.AccessToken;
                }
            }
        }

        public async Task DeleteTokenAsync(string username, string password, string token = null)
        {
            using (var client = _httpClientHelper.GetClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                    Base64Auth(username, password));

                using (
                    var response =
                        await
                            client.DeleteAsync(string.Format("/v1/access_tokens/{0}", token ?? _httpClientHelper.Token))
                    )
                {
                    response.EnsureSuccessStatusCode();

                    _httpClientHelper.Token = null;
                }
            }
        }

        public async Task<List<TokenData>> ListTokensAsync(string username, string password)
        {
            if (string.IsNullOrEmpty(username))
            {
                throw new ArgumentNullException("username");
            }

            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentNullException("password");
            }

            using (var client = _httpClientHelper.GetClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                    Base64Auth(username, password));

                using (var response = await client.GetAsync("/v1/access_tokens"))
                {
                    response.EnsureSuccessStatusCode();

                    return await response.Content.ReadAsAsync<List<TokenData>>();
                }
            }
        }

        public Device GetDevice(string deviceId)
        {
            return new Device(deviceId, _httpClientHelper);
        }

        public async Task<List<DeviceList>> ListDevicesAsync()
        {
            using (var client = _httpClientHelper.GetAuthorizedClient())
            {
                using (var response = await client.GetAsync("/v1/devices"))
                {
                    response.EnsureSuccessStatusCode();

                    return await response.Content.ReadAsAsync<List<DeviceList>>();
                }
            }
        }

        public async Task PublishEventAsync(string name, string data = null, bool @private = true, int ttl = 60)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("name");
            }

            using (var client = _httpClientHelper.GetAuthorizedClient())
            {
                var formValues = new Dictionary<string, string>
                {
                    {"name", name},
                    {"data", data},
                    {"private", @private.ToString()},
                    {"ttl", ttl.ToString()}
                }.ToList();

                using (
                    var response = await client.PostAsync("/v1/devices/events", new FormUrlEncodedContent(formValues)))
                {
                    response.EnsureSuccessStatusCode();
                }
            }
        }

#pragma warning disable 4014
        public async Task SubscribeAllDevicesEventsAsync()
        {
            using (var client = _httpClientHelper.GetAuthorizedClient())
            {
                client.Timeout = TimeSpan.FromMilliseconds(Timeout.Infinite);
                var stream = await client.GetStreamAsync("/v1/devices/events");

                _cancellationTokenSource = new CancellationTokenSource();

                var processor = new EventStreamProcessor(stream, _cancellationTokenSource);

                Task.Factory.StartNew(() => { processor.ProcessEvents(); },
                    _cancellationTokenSource.Token).ConfigureAwait(false);
            }
        }

        public Task UnsubscribeAllDevicesEventsAsync()
        {
            if (_cancellationTokenSource == null)
            {
                return Task.FromResult<object>(null);
            }

            var t = new Task(() => { _cancellationTokenSource.Cancel(); });

            t.Start();

            return t;
        }

        #endregion
    }
}