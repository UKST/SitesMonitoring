using System.Collections.Generic;

namespace SitesMonitoring.BLL.Data
{
    public interface IRepository<T> where T : EntityBase
    {
        T GetById(int id);
        IEnumerable<T> GetAll();
        void Create(T item);
        void Remove(T item);
    }
}