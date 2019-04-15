using System.ComponentModel.DataAnnotations;

namespace SitesMonitoring.API.Models.Sites
{
    public class SitePostModel
    {
        [Required]
        [MinLength(1)]
        [MaxLength(100)]
        public string Name { get; set; }
    }
}