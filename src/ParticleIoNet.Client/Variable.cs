using Newtonsoft.Json;

namespace ParticleIoNet.Client
{
    public class Variable
    {
        [JsonProperty(PropertyName = "cmd")]
        public string Cmd { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "result")]
        public object Result { get; set; }

        [JsonProperty(PropertyName = "coreInfo")]
        public DeviceDetails CoreInfo { get; set; }

        protected bool Equals(Variable other)
        {
            return string.Equals(Cmd, other.Cmd)
                   && string.Equals(Name, other.Name)
                   && Equals(Result.ToString(), other.Result.ToString())
                   && Equals(CoreInfo, other.CoreInfo);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((Variable) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (Cmd != null ? Cmd.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (Result != null ? Result.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (CoreInfo != null ? CoreInfo.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}