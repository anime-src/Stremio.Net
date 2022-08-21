using System;
using Newtonsoft.Json;

namespace Stremio.Net.Models.Metadata
{
    [Serializable]
    public class Meta : ICloneable
    {
        [JsonProperty("id", Required = Required.Always)]
        public string Id { get; set; } = null!;

        [JsonProperty("type", Required = Required.Always)]
        public MetaType? Type { get; set; }

        [JsonProperty("Name", Required = Required.Always)]
        public string Name { get; set; } = null!;

        [JsonProperty("genres")]
        [Obsolete("This will soon be deprecated in favor of links")]
        public string[]? Genres { get; set; }
        
        [JsonProperty("links")]
        public MetaLink[]? Links { get; set; }

        [JsonProperty("poster")]
        public string? Poster { get; set; }

        [JsonProperty("posterShape")]
        public PosterShape? PosterShape { get; set; }

        [JsonProperty("background")]
        public string? Background { get; set; }

        [JsonProperty("logo")]
        public string? Logo { get; set; }

        [JsonProperty("description")]
        public string? Description { get; set; }

        [JsonProperty("releaseInfo")]
        public string? ReleaseInfo { get; set; }

        [JsonProperty("director")]
        public string[]? Director { get; set; }

        [JsonProperty("cast")]
        public string[]? Cast { get; set; }

        [JsonProperty("imdbRating")]
        public string? ImdbRating { get; set; }

        [JsonProperty("dvdRelease")]
        public string? DvdRelease { get; set; }

        [JsonProperty("inTheaters")]
        public bool? InTheaters { get; set; }

        [JsonProperty("videos")]
        public Video[]? Videos { get; set; }

        [JsonProperty("certification")]
        public string? Certification { get; set; }

        [JsonProperty("runtime")]
        public string? Runtime { get; set; }

        [JsonProperty("language")]
        public string? Language { get; set; }

        [JsonProperty("country")]
        public string? Country { get; set; }

        [JsonProperty("awards")]
        public string? Awards { get; set; }

        [JsonProperty("website")]
        public string? Website { get; set; }

        [JsonProperty("isPeered")]
        public string? IsPeered { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
