using System.Collections.Generic;
using SitesMonitoring.BLL.Data;

namespace SitesMonitoring.DAL
{
    public class Repository : IRepository<string>
    {
        public string GetById(int id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<string> GetAll()
        {
            return new[] {"1", "2", "3"};
        }

        public void Create(string item)
        {
            throw new System.NotImplementedException();
        }

        public void Remove(string item)
        {
            throw new System.NotImplementedException();
        }
    }
}