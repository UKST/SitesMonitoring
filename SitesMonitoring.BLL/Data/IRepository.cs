using System.Collections.Generic;
using System.Threading.Tasks;

namespace SitesMonitoring.BLL.Data
{
    public interface IRepository<T> where T : EntityBase
    {
        Task<T> GetByIdAsync(long id);
        Task<IEnumerable<T>> GetAllAsync();
        Task CreateAsync(T item);
        Task RemoveAsync(T item);
    }
}