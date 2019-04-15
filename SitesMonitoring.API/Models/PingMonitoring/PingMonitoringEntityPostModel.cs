using System;
using System.ComponentModel.DataAnnotations;

namespace SitesMonitoring.API.Models.PingMonitoring
{
    public class PingMonitoringEntityPostModel
    {
        [Required]
        public int? PeriodInMinutes { get; set; }
        
        [Required]
        [MinLength(4)]
        [MaxLength(255 )]
        public string Address { get; set; }
    }
}