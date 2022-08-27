using System;
using Newtonsoft.Json;

namespace Stremio.Net.Models.Catalogs
{
    public class Extra
    {
        [JsonProperty("name", Required = Required.Always)]
        public string Name { get; set; } = null!;

        [JsonProperty("isRequired")] 
        public bool? IsRequired { get; set; }
    
        [JsonProperty("options")] 
        public string[]? Options { get; set; }
    
        [JsonProperty("optionsLimit")] 
        public int? OptionsLimit { get; set; }
    
        [JsonIgnore]
        public Type? ExpectedType { get; set; }
    }
}