using System.Collections.Generic;

namespace SitesMonitoring.BLL.Data
{
    public interface IRepository
    {
        IEnumerable<string> GetAll();
    }
}