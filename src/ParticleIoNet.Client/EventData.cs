using System;
using System.Globalization;
using Newtonsoft.Json;

namespace ParticleIoNet.Client
{
    public class EventData
    {
        [JsonIgnore]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "data")]
        public string Data { get; set; }

        [JsonProperty(PropertyName = "ttl")]
        public int Ttl { get; set; }

        [JsonProperty(PropertyName = "published_at")]
        public DateTimeOffset PublishedAt { get; set; }

        [JsonProperty(PropertyName = "coreid")]
        public string DeviceId { get; set; }

        protected bool Equals(EventData other)
        {
            return string.Equals(Name, other.Name)
                   && string.Equals(Data, other.Data)
                   && Ttl == other.Ttl
                   &&
                   PublishedAt.UtcDateTime.ToString(CultureInfo.InvariantCulture)
                       .Equals(other.PublishedAt.UtcDateTime.ToString(CultureInfo.InvariantCulture))
                   && string.Equals(DeviceId, other.DeviceId);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((EventData) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (Data != null ? Data.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ Ttl;
                hashCode = (hashCode*397) ^ PublishedAt.GetHashCode();
                hashCode = (hashCode*397) ^ (DeviceId != null ? DeviceId.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}