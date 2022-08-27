using Newtonsoft.Json;

namespace Stremio.Net.Models.Catalogs
{
    public class Catalog
    {
        [JsonProperty("id", Required = Required.Always)]
        public string Id { get; set; } = null!;

        [JsonProperty("name")] 
        public string? Name { get; set; }

        [JsonProperty("type", Required = Required.Always)]
        public string Type { get; set; } = null!;
    
        [JsonProperty("extra")] 
        public Extra[]? Extra { get; set; }
    }
}