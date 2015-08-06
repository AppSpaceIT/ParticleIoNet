using Newtonsoft.Json;

namespace ParticleIoNet.Client
{
    internal class CreateToken
    {
        [JsonProperty(PropertyName = "token_type")]
        public string TokenType { get; set; }

        [JsonProperty(PropertyName = "access_token")]
        public string AccessToken { get; set; }

        [JsonProperty(PropertyName = "expires_in")]
        public long ExpiresIn { get; set; }

        [JsonProperty(PropertyName = "refresh_token")]
        public string Refreshtoken { get; set; }
    }
}