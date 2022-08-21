using System;
using Newtonsoft.Json;
using Stremio.Net.Models.Streams;

namespace Stremio.Net.Models.Metadata;

[Serializable]
public class Video
{
    [JsonProperty("id", Required = Required.Always)]
    public string Id { get; set; } = null!;

    [JsonProperty("title", Required = Required.Always)]
    public string Title { get; set; } = null!;

    [JsonProperty("released", Required = Required.Always)]
    public string Released { get; set; } = null!;

    [JsonProperty("thumbnail")]
    public string? Thumbnail { get; set; }

    [JsonProperty("streams")]
    public Stream[]? Streams { get; set; }

    [JsonProperty("available")]
    public bool? Available { get; set; }

    [JsonProperty("season")]
    public int? Season { get; set; }        

    [JsonProperty("episode")]
    public int? Episode { get; set; }

    [JsonProperty("trailer")]
    public string? Trailer { get; set; }

    [JsonProperty("overview")]
    public string? Overview { get; set; }
}