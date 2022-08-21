using System;
using Newtonsoft.Json;
using Stremio.Net.Models.Catalogs;

namespace Stremio.Net.Models
{
    [Serializable]
    public class Manifest
    {
        [JsonProperty("id", Required = Required.Always)]
        public string Id { get; set; } = null!;

        [JsonProperty("name", Required = Required.Always)]
        public string Name { get; set; } = null!;

        [JsonProperty("description", Required = Required.Always)]
        public string Description { get; set; } = null!;

        [JsonProperty("version", Required = Required.Always)]
        public string Version { get; set; } = null!;

        [JsonProperty("types", Required = Required.Always)]
        public string[] Types { get; set; } = Array.Empty<string>();

        [JsonProperty("catalogs", Required = Required.Always)]
        public Catalog[] Catalogs { get; set; } = Array.Empty<Catalog>();

        [JsonProperty("resources", Required = Required.Always)]
        public string[] Resources { get; set; } = Array.Empty<string>();

        [JsonProperty("idPrefixes")] 
        public string[]? IdPrefixes { get; set; }
    }
}