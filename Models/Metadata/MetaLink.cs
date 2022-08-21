using System;
using Newtonsoft.Json;

namespace Stremio.Net.Models.Metadata;

[Serializable]
public class MetaLink
{
    [JsonProperty("name", Required = Required.Always)]
    public string Name { get; set; } = null!;

    [JsonProperty("category", Required = Required.Always)]
    public string Category { get; set; } = null!;

    [JsonProperty("url", Required = Required.Always)]
    public string Url { get; set; } = null!;
}