using System.Collections.Generic;
using System.Linq;
using SitesMonitoring.BLL.Data;

namespace SitesMonitoring.DAL
{
    public class Repository<T> : IRepository<T> where T : EntityBase
    {
        // todo - add async support
        protected SitesMonitoringDbContext Db { get; }

        public Repository(SitesMonitoringDbContext db)
        {
            Db = db;
        }
        
        public T GetById(long id)
        {
            return Db.Set<T>().Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return Db.Set<T>().ToArray();
        }

        public void Create(T item)
        {
            Db.Set<T>().Add(item);
            Db.SaveChanges(); // todo - move to UoW          
        }

        public void Remove(T item)
        {
            Db.Set<T>().Remove(item);
            Db.SaveChanges(); // todo - move to UoW
        }
    }
}