using System.Collections.Generic;

namespace SitesMonitoring.BLL.Endpoints
{
    public interface IHandler
    {
        ICollection<string> HandleGet();
    }
}