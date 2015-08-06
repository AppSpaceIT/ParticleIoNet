using System;
using Newtonsoft.Json;

namespace ParticleIoNet.Client
{
    public class DeviceDetails
    {
        [JsonProperty(PropertyName = "last_app")]
        public string LastApp { get; set; }

        [JsonProperty(PropertyName = "last_heard")]
        public DateTimeOffset? LastHeard { get; set; }

        [JsonProperty(PropertyName = "last_handshake_at")]
        public DateTimeOffset? LastHandshake { get; set; }

        [JsonProperty(PropertyName = "connected")]
        public bool Connected { get; set; }

        [JsonProperty(PropertyName = "deviceID")]
        public string DeviceId { get; set; }

        protected bool Equals(DeviceDetails other)
        {
            return string.Equals(LastApp, other.LastApp)
                   && LastHeard.Equals(other.LastHeard)
                   && LastHandshake.Equals(other.LastHandshake)
                   && Connected == other.Connected
                   && string.Equals(DeviceId, other.DeviceId);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((DeviceDetails) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (LastApp != null ? LastApp.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ LastHeard.GetHashCode();
                hashCode = (hashCode*397) ^ LastHandshake.GetHashCode();
                hashCode = (hashCode*397) ^ Connected.GetHashCode();
                hashCode = (hashCode*397) ^ (DeviceId != null ? DeviceId.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}