using Newtonsoft.Json;

namespace ParticleIoNet.Client
{
    public class FlashFirmware
    {
        [JsonProperty(PropertyName = "ok")]
        internal bool Ok { get; set; }

        public bool Success
        {
            get { return Ok && Message == "Update started" || Status == "Update started"; }
        }

        [JsonProperty(PropertyName = "message")]
        internal string Message { get; set; }

        [JsonProperty(PropertyName = "status")]
        internal string Status { get; set; }

        [JsonProperty(PropertyName = "errors")]
        public dynamic Errors { get; set; }

        protected bool Equals(FlashFirmware other)
        {
            return Ok == other.Ok && string.Equals(Message, other.Message) && string.Equals(Status, other.Status) &&
                   Equals(Errors, other.Errors);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((FlashFirmware) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Ok.GetHashCode();
                hashCode = (hashCode*397) ^ (Message != null ? Message.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (Status != null ? Status.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (Errors != null ? Errors.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}