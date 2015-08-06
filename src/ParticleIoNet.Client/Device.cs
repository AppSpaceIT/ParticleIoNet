using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace ParticleIoNet.Client
{
    public class EventPublishedArgs : EventArgs
    {
        public EventData EventData { get; set; }
    }

    public class Device
    {
        private readonly HttpClientHelper _httpClientHelper;
        private CancellationTokenSource _cancellationTokenSource;

        internal Device(string deviceId, HttpClientHelper httpClientHelper)
        {
            DeviceId = deviceId;
            _httpClientHelper = httpClientHelper;
        }

        public string DeviceId { get; set; }

        public async Task ClaimAsync()
        {
            using (var client = _httpClientHelper.GetAuthorizedClient())
            {
                var formValues = new Dictionary<string, string>
                {
                    {"id", DeviceId}
                }.ToList();

                using (var response = await client.PostAsync("/v1/devices", new FormUrlEncodedContent(formValues)))
                {
                    response.EnsureSuccessStatusCode();
                }
            }
        }

        public async Task RemoveAsync()
        {
            using (var client = _httpClientHelper.GetAuthorizedClient())
            {
                using (var response = await client.DeleteAsync(string.Format("/v1/devices/{0}", DeviceId)))
                {
                    response.EnsureSuccessStatusCode();
                }
            }
        }

        public async Task<DeviceInfo> GetInfoAsync()
        {
            using (var client = _httpClientHelper.GetAuthorizedClient())
            {
                using (var response = await client.GetAsync(string.Format("/v1/devices/{0}", DeviceId)))
                {
                    response.EnsureSuccessStatusCode();

                    return await response.Content.ReadAsAsync<DeviceInfo>();
                }
            }
        }

        public async Task<Variable> ReadVariableAsync(string variableName)
        {
            if (string.IsNullOrEmpty(variableName))
            {
                throw new ArgumentNullException("variableName");
            }

            using (var client = _httpClientHelper.GetAuthorizedClient())
            {
                using (
                    var response = await client.GetAsync(string.Format("/v1/devices/{0}/{1}", DeviceId, variableName)))
                {
                    response.EnsureSuccessStatusCode();

                    return await response.Content.ReadAsAsync<Variable>();
                }
            }
        }

        public async Task<CallFunction> CallFunctionAsync(string functionName,
            params string[] arguments)
        {
            if (string.IsNullOrEmpty(functionName))
            {
                throw new ArgumentNullException("functionName");
            }

            using (var client = _httpClientHelper.GetAuthorizedClient())
            {
                var args = arguments.ToList().Aggregate((current, next) => current + "," + next);

                var formValues = new Dictionary<string, string>
                {
                    {"args", args}
                }.ToList();

                using (
                    var response =
                        await
                            client.PostAsync(string.Format("/v1/devices/{0}/{1}", DeviceId, functionName),
                                new FormUrlEncodedContent(formValues)))
                {
                    response.EnsureSuccessStatusCode();

                    return await response.Content.ReadAsAsync<CallFunction>();
                }
            }
        }
#pragma warning disable 4014
        public async Task SubscribeEventsAsync(string deviceId)
        {
            if (string.IsNullOrEmpty(deviceId))
            {
                throw new ArgumentNullException("deviceId");
            }

            using (var client = _httpClientHelper.GetAuthorizedClient())
            {
                client.Timeout = TimeSpan.FromMilliseconds(Timeout.Infinite);
                var stream = await client.GetStreamAsync(string.Format("/v1/devices/{0}/events", deviceId));

                _cancellationTokenSource = new CancellationTokenSource();

                var processor = new EventStreamProcessor(stream, _cancellationTokenSource);

                Task.Factory.StartNew(() => { processor.ProcessEvents(); },
                    _cancellationTokenSource.Token).ConfigureAwait(false);
            }
        }

        public Task UnsubscribeEventsAsync()
        {
            if (_cancellationTokenSource == null)
            {
                return Task.FromResult<object>(null);
            }

            var t = new Task(() => { _cancellationTokenSource.Cancel(); });

            t.Start();

            return t;
        }

        public async Task<FlashFirmware> FlashBinaryFirmareAsync(byte[] firmware)
        {
            return await InternalFlashFirmwareAsync(new Dictionary<string, byte[]>(1) {{"file", firmware}}, true);
        }

        public async Task<FlashFirmware> FlashFirmwareAsync(IReadOnlyDictionary<string, byte[]> firmware)
        {
            return await InternalFlashFirmwareAsync(firmware, false);
        }

        private async Task<FlashFirmware> InternalFlashFirmwareAsync(IReadOnlyDictionary<string, byte[]> firmware,
            bool isBinary)
        {
            using (var client = _httpClientHelper.GetAuthorizedClient())
            {
                var content =
                    new MultipartFormDataContent("---------------------------" + DateTime.Now.Ticks.ToString("x"));

                var i = 0;
                foreach (var f in firmware)
                {
                    var fileId = string.Format("\"file{0}\"", i == 0 ? "" : (i + 1).ToString());
                    content.Add(new ByteArrayContent(f.Value), fileId, string.Format("\"{0}\"", f.Key));
                    i++;
                }

                if (isBinary)
                {
                    content.Add(new StringContent("file_type=\"binary\""));
                }

                using (var response = await client.PutAsync(string.Format("/v1/devices/{0}", DeviceId), content))
                {
                    response.EnsureSuccessStatusCode();

                    return await response.Content.ReadAsAsync<FlashFirmware>();
                }
            }
        }
    }
}