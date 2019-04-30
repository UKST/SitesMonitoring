using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using SitesMonitoring.BLL.Data;

namespace SitesMonitoring.BLL.Monitoring
{
    public class MonitoringResult : EntityBase
    {
        [Column(TypeName = "jsonb")]
        public string Data { get; set; }
        public DateTime CreatedDate { get; set; }
        public long MonitoringEntityId { get; set; }

        public MonitoringEntity MonitoringEntity { get; set; }
        
        public void SetData<T>(T data)
        {
            Data = JsonConvert.SerializeObject(data);
        }
        
        public T GetData<T>()
        {
            return JsonConvert.DeserializeObject<T>(Data);
        }
    }
}