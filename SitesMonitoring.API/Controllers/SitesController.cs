using Microsoft.AspNetCore.Mvc;
using SitesMonitoring.API.Models.Sites;

namespace SitesMonitoring.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SitesController : ControllerBase
    {
        [HttpPost]
        public void Post([FromBody] SitePostModel model)
        {
            
        }
    }
}