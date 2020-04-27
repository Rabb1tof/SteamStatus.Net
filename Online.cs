using Newtonsoft.Json;

namespace SteamStatus.Net
{
    public class Online
    {
        [JsonProperty("status")]
        public Status Status { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }
    }
}
