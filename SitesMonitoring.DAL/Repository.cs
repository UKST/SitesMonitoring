using System.Collections.Generic;
using SitesMonitoring.BLL.Data;

namespace SitesMonitoring.DAL
{
    public class Repository : IRepository
    {
        public IEnumerable<string> GetAll()
        {
            return new[] {"1", "2", "3"};
        }
    }
}