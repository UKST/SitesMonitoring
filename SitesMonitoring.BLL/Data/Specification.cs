using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SitesMonitoring.BLL.Data
{
    public class Specification<T> : ISpecification<T>
    {
        public Specification(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }
        public Expression<Func<T, bool>> Criteria { get; }

        public ICollection<Expression<Func<T, object>>> Includes { get; } =
            new List<Expression<Func<T, object>>>();

        public ICollection<string> IncludeStrings { get; } = new List<string>();

        public ISpecification<T> AddInclude(Expression<Func<T, object>> includeExpression)
        {
            Includes.Add(includeExpression);
            return this;
        }

        public ISpecification<T> AddInclude(string includeString)
        {
            IncludeStrings.Add(includeString);
            return this;
        }
    }
}