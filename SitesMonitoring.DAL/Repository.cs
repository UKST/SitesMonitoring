using System.Collections.Generic;
using System.Linq;
using SitesMonitoring.BLL.Data;

namespace SitesMonitoring.DAL
{
    public class Repository<T> : IRepository<T> where T : EntityBase
    {
        // todo - add async support
        private readonly SitesMonitoringDbContext _db;
        
        public Repository(SitesMonitoringDbContext db)
        {
            _db = db;
        }
        
        public T GetById(long id)
        {
            return _db.Set<T>().Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return _db.Set<T>().ToArray();
        }

        public void Create(T item)
        {
            _db.Set<T>().Add(item);
            _db.SaveChanges(); // todo - move to UoW          
        }

        public void Remove(T item)
        {
            _db.Set<T>().Remove(item);
            _db.SaveChanges(); // todo - move to UoW
        }
    }
}