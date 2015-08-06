using System;
using System.IO;
using System.Text;
using System.Threading;
using Newtonsoft.Json;

namespace ParticleIoNet.Client
{
    public class EventStreamProcessor
    {
        private readonly byte[] _buffer = new byte[2048];
        private readonly CancellationTokenSource _cancellationTokenSource;
        private readonly StringBuilder _chunks = new StringBuilder();
        private readonly UTF8Encoding _encoder = new UTF8Encoding();
        private readonly Stream _stream;
        private string _eventName;

        public EventStreamProcessor(Stream stream, CancellationTokenSource cancellationTokenSource)
        {
            _stream = stream;
            _cancellationTokenSource = cancellationTokenSource;
        }

        public void ProcessEvents()
        {
            var processor = new EventStreamProcessor(_stream, _cancellationTokenSource);

            while (_stream.CanRead && !_cancellationTokenSource.IsCancellationRequested)
            {
                var eventData = processor.GetEvent();

                if (eventData == null)
                {
                    continue;
                }

                OnEventPublished(new EventPublishedArgs {EventData = eventData});
            }
        }

        public EventData GetEvent()
        {
            var len = _stream.Read(_buffer, 0, 2048);

            if (len <= 0)
            {
                return null;
            }

            var text = _encoder.GetString(_buffer, 0, len);

            if (text.StartsWith("event:"))
            {
                _eventName = text.Replace("event: ", "").Trim();

                return null;
            }

            if (!text.StartsWith("data:"))
            {
                return null;
            }

            _chunks.Append(text.Replace("data: ", "").Trim());

            EventData eventData = null;
            try
            {
                eventData = JsonConvert.DeserializeObject<EventData>(_chunks.ToString());
                eventData.Name = _eventName;
            }
            catch (Exception)
            {
                // ignored
            }

            _chunks.Clear();

            return eventData;
        }

        #region events

        public event EventHandler<EventPublishedArgs> EventPublished;

        protected virtual void OnEventPublished(EventPublishedArgs e)
        {
            var handler = EventPublished;

            if (handler != null)
            {
                handler(this, e);
            }
        }

        #endregion
    }
}