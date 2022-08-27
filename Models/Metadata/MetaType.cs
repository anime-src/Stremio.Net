using System.Runtime.Serialization;

namespace Stremio.Net.Models.Metadata
{
    public enum MetaType
    {
        [EnumMember(Value = "movie")]
        Movie,
        [EnumMember(Value = "series")]
        Series,
        [EnumMember(Value = "channel")]
        Channel,
        [EnumMember(Value = "tv")]
        Tv
    }
}