using System.Collections.Generic;
using System.Threading.Tasks;

namespace SitesMonitoring.BLL.Data
{
    public interface IRepository<T>
    {
        Task<T> GetFirstOrDefaultAsync(ISpecification<T> specification);
        Task<IEnumerable<T>> GetManyAsync(ISpecification<T> specification);
        void Create(T item);
        void Remove(T item);
        Task SaveChangesAsync();
    }
}