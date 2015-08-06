using System;
using Newtonsoft.Json;

namespace ParticleIoNet.Client
{
    public class TokenData
    {
        [JsonProperty(PropertyName = "token")]
        public string Token { get; set; }

        [JsonProperty(PropertyName = "expires_at")]
        public DateTimeOffset? ExpiresAt { get; set; }

        [JsonProperty(PropertyName = "client")]
        public string Client { get; set; }

        protected bool Equals(TokenData other)
        {
            return string.Equals(Token, other.Token)
                   && ExpiresAt.Equals(other.ExpiresAt)
                   && string.Equals(Client, other.Client);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((TokenData) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (Token != null ? Token.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ ExpiresAt.GetHashCode();
                hashCode = (hashCode*397) ^ (Client != null ? Client.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}