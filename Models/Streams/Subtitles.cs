using System;
using Newtonsoft.Json;

namespace Stremio.Net.Models.Streams
{
    [Serializable]
    public class Subtitles
    {
        [JsonProperty("id", Required = Required.Always)]
        public string Id { get; set; } = null!;

        [JsonProperty("url", Required = Required.Always)]
        public string Url { get; set; } = null!;

        [JsonProperty("lang", Required = Required.Always)]
        public string Lang { get; set; } = null!;
    }
}