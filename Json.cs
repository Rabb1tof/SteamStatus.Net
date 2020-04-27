using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace SteamStatus.Net
{
    public class Json
    {
        [JsonProperty("time")] public int Time { get; set; }
        [JsonProperty("online")] public double Online { get; set; }

        /// <summary>
        /// @param - id (csgo, store, etc...)
        /// @param - bool (die?)
        /// @param - status (normal, ok, etc...)
        /// </summary>
        [JsonProperty("services")]
        public List<List<object>> Services { get; set; }
        //public Dictionary<string, Online> Services { get; set; }

        public DateTime CurrentTime => DateTimeOffset.FromUnixTimeSeconds(this.Time).DateTime;

        public Dictionary<string, string> ServicesDictionary()
        {
            var dict = new Dictionary<string, string>();
            foreach (var service in Services)
            {
                dict.Add((string) service[0], (string) service[2]);
            }

            return dict;
        }
}
}
