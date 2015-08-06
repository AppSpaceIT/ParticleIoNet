using Newtonsoft.Json;

namespace ParticleIoNet.Client
{
    public class CallFunction
    {
        [JsonProperty(PropertyName = "id")]
        public string DeviceId { get; set; }

        [JsonProperty(PropertyName = "last_app")]
        public string LastApp { get; set; }

        [JsonProperty(PropertyName = "connected")]
        public bool Connected { get; set; }

        [JsonProperty(PropertyName = "return_value")]
        public int ReturnValue { get; set; }

        protected bool Equals(CallFunction other)
        {
            return string.Equals(DeviceId, other.DeviceId)
                   && string.Equals(LastApp, other.LastApp)
                   && Connected == other.Connected
                   && ReturnValue == other.ReturnValue;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((CallFunction) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (DeviceId != null ? DeviceId.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (LastApp != null ? LastApp.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ Connected.GetHashCode();
                hashCode = (hashCode*397) ^ ReturnValue;
                return hashCode;
            }
        }
    }
}