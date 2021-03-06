using System.Collections.Generic;
using Newtonsoft.Json;

namespace SitesMonitoring.API.Models
{
    public sealed class GenericErrorsResultModel
    {
        [JsonProperty(PropertyName = "errors")]
        public ICollection<string> Errors { get; set; }
    }
}