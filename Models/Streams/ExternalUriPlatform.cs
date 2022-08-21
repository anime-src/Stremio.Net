using System.Runtime.Serialization;

namespace Stremio.Net.Models.Streams;

public enum ExternalUriPlatform
{
    [EnumMember(Value = "android")]
    Android,
    [EnumMember(Value = "ios")]
    Ios
}