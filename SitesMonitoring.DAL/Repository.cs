using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SitesMonitoring.BLL.Data;

namespace SitesMonitoring.DAL
{
    public class Repository<T> : IRepository<T> where T : EntityBase
    {
        protected SitesMonitoringDbContext Db { get; }

        public Repository(SitesMonitoringDbContext db)
        {
            Db = db;
        }

        public async Task<T> GetFirstOrDefaultAsync(ISpecification<T> specification)
        {
            return await ApplySpecification(specification)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> GetManyAsync(ISpecification<T> specification)
        {
            return await ApplySpecification(specification)
                .ToArrayAsync();
        }

        public void Create(T item)
        {
            Db.Set<T>().AddAsync(item);
        }

        public void Remove(T item)
        {
            Db.Set<T>().Remove(item);
        }

        public async Task SaveChangesAsync()
        {
            await Db.SaveChangesAsync();
        }

        private IQueryable<T> ApplySpecification(ISpecification<T> specification)
        {
            var queryableResultWithIncludes = specification.Includes
                .Aggregate(Db.Set<T>().AsQueryable(),
                    (current, include) => current.Include(include));

            var secondaryResult = specification.IncludeStrings
                .Aggregate(queryableResultWithIncludes,
                    (current, include) => current.Include(include));

            return secondaryResult
                .Where(specification.Criteria);
        }
    }
}