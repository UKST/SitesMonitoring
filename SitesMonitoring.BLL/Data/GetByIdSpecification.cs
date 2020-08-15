using System;
using System.Linq.Expressions;

namespace SitesMonitoring.BLL.Data
{
    internal sealed class GetByIdSpecification<T> : Specification<T> where T : EntityBase
    {
        public GetByIdSpecification(long id) : base(i => i.Id == id)
        {
        }
    }
}