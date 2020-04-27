using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace SteamStatus.Net
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Status
    {
        [EnumMember(Value = "good")]
        Good,

        [EnumMember(Value = "minor")]
        Minor,

        [EnumMember(Value = "major")]
        Major
    }
}
