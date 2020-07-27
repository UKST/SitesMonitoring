using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace SitesMonitoring.Utils.Http
{
    public static class HttpRequestMessageExtensions
    {
        public static HttpRequestMessage FromObject(this HttpRequestMessage httpRequestMessage, object request)
        {
            httpRequestMessage.Content =
                new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            return httpRequestMessage;
        }
    }
}