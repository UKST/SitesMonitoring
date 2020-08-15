using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SitesMonitoring.BLL.Data
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> Criteria { get; }
        ICollection<Expression<Func<T, object>>> Includes { get; }
        ICollection<string> IncludeStrings { get; }
    }
}