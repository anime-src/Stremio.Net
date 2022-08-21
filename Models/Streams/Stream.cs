using System;
using Newtonsoft.Json;

namespace Stremio.Net.Models.Streams
{
    [Serializable]
    public class Stream
    {
        [JsonProperty("url")]
        public string? Url { get; set; }

        [JsonProperty("ytId")]
        public string? YtId { get; set; }

        [JsonProperty("infoHash")]
        public string? InfoHash { get; set; }
        
        [JsonProperty("fileIdx")]
        public uint? FileIdx { get; set; }

        [JsonProperty("mapIdx")]
        public uint? MapIdx { get; set; }

        [JsonProperty("externalUrl")]
        public string? ExternalUrl { get; set; }

        [JsonProperty("externalUris")]
        public ExternalUri[]? ExternalUris { get; set; }

        [JsonProperty("name")]
        public string? Name { get; set; }

        [JsonProperty("title")]
        [Obsolete("This will soon be deprecated in favor of Description")]
        public string? Title { get; set; }

        [JsonProperty("description")]
        public string? Description { get; set; }
        
        [JsonProperty("availability")]
        public Availability? Availability { get; set; }

        [JsonProperty("tag")]
        public string? Tag { get; set; }

        [JsonProperty("isFree")]
        public bool? IsFree { get; set; }

        [JsonProperty("isSubscription")]
        public bool? IsSubscription { get; set; }

        [JsonProperty("isPeered")]
        public bool? IsPeered { get; set; }

        [JsonProperty("subtitles")]
        public Subtitles[]? Subtitles { get; set; }

        [JsonProperty("subtitlesExclusive")]
        public bool? SubtitlesExclusive { get; set; }

        [JsonProperty("live")]
        public bool? Live { get; set; }

        [JsonProperty("repeat")]
        public bool? Repeat { get; set; }

        [JsonProperty("geos")]
        public string[]? Geos { get; set; }

        [JsonProperty("meta")]
        public object? Meta { get; set; }
    }
}
