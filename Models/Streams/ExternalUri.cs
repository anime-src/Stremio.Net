using System;
using Newtonsoft.Json;

namespace Stremio.Net.Models.Streams
{
    [Serializable]
    public class ExternalUri
    {
        [JsonProperty("platform")]
        public ExternalUriPlatform? Platform { get; set; }

        [JsonProperty("uri")]
        public string? Uri { get; set; }

        [JsonProperty("appUri")]
        public string? AppUri { get; set; }

    }
}