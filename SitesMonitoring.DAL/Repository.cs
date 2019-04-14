using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using SitesMonitoring.BLL.Data;

namespace SitesMonitoring.DAL
{
    public class Repository<T> : IRepository<T> where T : EntityBase
    {
        private long _id = 1;
        
        protected List<T> Entities { get; } = new List<T>();

        public T GetById(int id)
        {
            return Entities.Single(i => i.Id == id);
        }

        public IEnumerable<T> GetAll()
        {
            return Entities;
        }

        public void Create(T item)
        {
            // emulate identity property in database
            if (item.Id != 0) 
                throw new ArgumentException("Identity insert not supported");
            
            item.Id = _id++;
            Entities.Add(item);
        }

        public void Remove(T item)
        {
            Entities.Remove(item);
        }
    }
}