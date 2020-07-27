using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SitesMonitoring.Utils.Http
{
    public static class HttpResponseMessageExtensions
    {
        public static async Task<T> ReadAsync<T>(this HttpResponseMessage responseMessage)
        {
            var response = await responseMessage.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(response);
        }
    }
}