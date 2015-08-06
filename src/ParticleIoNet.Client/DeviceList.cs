using System;
using Newtonsoft.Json;

namespace ParticleIoNet.Client
{
    public class DeviceList
    {
        [JsonProperty(PropertyName = "id")]
        public string DeviceId { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "last_app")]
        public string LastApp { get; set; }

        [JsonProperty(PropertyName = "last_ip_address")]
        public string LastIpAddress { get; set; }

        [JsonProperty(PropertyName = "last_heard")]
        public DateTimeOffset? LastHeard { get; set; }

        [JsonProperty(PropertyName = "product_id")]
        public int ProductId { get; set; }

        [JsonProperty(PropertyName = "connected")]
        public bool Connected { get; set; }

        protected bool Equals(DeviceList other)
        {
            return string.Equals(DeviceId, other.DeviceId)
                   && string.Equals(Name, other.Name)
                   && string.Equals(LastApp, other.LastApp)
                   && string.Equals(LastIpAddress, other.LastIpAddress)
                   && LastHeard.Equals(other.LastHeard)
                   && ProductId.Equals(other.ProductId)
                   && Connected == other.Connected;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((DeviceList) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (DeviceId != null ? DeviceId.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (LastApp != null ? LastApp.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (LastIpAddress != null ? LastIpAddress.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ LastHeard.GetHashCode();
                hashCode = (hashCode*397) ^ ProductId.GetHashCode();
                hashCode = (hashCode*397) ^ Connected.GetHashCode();
                return hashCode;
            }
        }
    }
}