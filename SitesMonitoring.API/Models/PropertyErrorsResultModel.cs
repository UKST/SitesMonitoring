using System.Collections.Generic;
using Newtonsoft.Json;

namespace SitesMonitoring.API.Models
{
    public class PropertyErrorsResultModel
    {
        [JsonProperty(PropertyName = "errors")]
        public IDictionary<string, IEnumerable<string>> PropertyErrors { get; set; }
    }
}