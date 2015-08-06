using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Newtonsoft.Json;

namespace ParticleIoNet.Client
{
    public class DeviceInfo
    {
        [JsonProperty(PropertyName = "id")]
        public string DeviceId { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "connected")]
        public bool Connected { get; set; }

        [JsonProperty(PropertyName = "variables")]
        public Dictionary<string, string> Variables { get; set; }

        [JsonProperty(PropertyName = "functions")]
        public string[] Functions { get; set; }

        [JsonProperty(PropertyName = "cc3000_patch_version")]
        public string Cc3000PatchVersion { get; set; }

        [JsonProperty(PropertyName = "product_id")]
        public int ProductId { get; set; }

        [JsonProperty(PropertyName = "last_heard")]
        public DateTimeOffset? LastHeard { get; set; }

        protected bool Equals(DeviceInfo other)
        {
            return string.Equals(DeviceId, other.DeviceId) && string.Equals(Name, other.Name) &&
                   Connected == other.Connected &&
                   Variables.SequenceEqual(other.Variables) && Functions.SequenceEqual(other.Functions) &&
                   string.Equals(Cc3000PatchVersion, other.Cc3000PatchVersion) && ProductId == other.ProductId &&
                   LastHeard.GetValueOrDefault()
                       .ToString(CultureInfo.InvariantCulture)
                       .Equals(other.LastHeard.GetValueOrDefault().ToString(CultureInfo.InvariantCulture));
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((DeviceInfo) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (DeviceId != null ? DeviceId.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ Connected.GetHashCode();
                hashCode = (hashCode*397) ^ (Variables != null ? Variables.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (Functions != null ? Functions.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (Cc3000PatchVersion != null ? Cc3000PatchVersion.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ ProductId;
                hashCode = (hashCode*397) ^ LastHeard.GetHashCode();
                return hashCode;
            }
        }
    }
}