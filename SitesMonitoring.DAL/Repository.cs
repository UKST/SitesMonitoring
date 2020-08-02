using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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

        public async Task<T> GetByIdAsync(long id)
        {
            return await Db.Set<T>().FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await Db.Set<T>().ToArrayAsync();
        }

        public async Task CreateAsync(T item)
        {
            await Db.Set<T>().AddAsync(item);
            await Db.SaveChangesAsync(); // todo - move to UoW
        }

        public async Task RemoveAsync(T item)
        {
            Db.Set<T>().Remove(item);
            await Db.SaveChangesAsync(); // todo - move to UoW
        }
    }
}