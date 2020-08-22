using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SitesMonitoring.BLL.Data;

namespace SitesMonitoring.DAL
{
    public class Repository<T> : IRepository<T> where T : EntityBase
    {
        private readonly SitesMonitoringDbContext _db;

        public Repository(SitesMonitoringDbContext db)
        {
            _db = db;
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
            _db.Set<T>().AddAsync(item);
        }

        public void Remove(T item)
        {
            _db.Set<T>().Remove(item);
        }

        public async Task SaveChangesAsync()
        {
            await _db.SaveChangesAsync();
        }

        private IQueryable<T> ApplySpecification(ISpecification<T> specification)
        {
            var queryableResultWithIncludes = specification.Includes
                .Aggregate(_db.Set<T>().AsQueryable(),
                    (current, include) => current.Include(include));

            var secondaryResult = specification.IncludeStrings
                .Aggregate(queryableResultWithIncludes,
                    (current, include) => current.Include(include));

            return secondaryResult
                .Where(specification.Criteria);
        }
    }
}