using System.Runtime.Serialization;

namespace Stremio.Net.Models.Metadata
{
    public enum PosterShape
    {
        [EnumMember(Value = "square")]
        Square,
        [EnumMember(Value = "regular")]
        Regular,
        [EnumMember(Value = "landscape")]
        Landscape
    }
}