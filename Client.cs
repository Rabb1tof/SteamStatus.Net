using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SteamStatus.Net
{

    public class Client
    {
        public async Task<Json> GetAsync()
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("Microsoft.CSharp", "1.0"));
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls |
                                                       SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                
                return JsonConvert.DeserializeObject<Json>(
                    await client.GetStringAsync("https://crowbar.steamstat.us/gravity.json"));
            }
        }
    }
}
