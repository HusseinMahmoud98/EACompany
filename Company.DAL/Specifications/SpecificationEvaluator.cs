using Company.BLL.Services.Interfaces.Specification;
using Company.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.DAL.Specifications
{
    public static class SpecificationEvaluator<TKey ,TEntity> where TEntity : BaseEntity<TKey>
    {
       
            public static IQueryable<TEntity> GetQuery(
                IQueryable<TEntity> inputQuery,
                ISpecification<TKey, TEntity> spec)
            {
                var query = inputQuery;

                if (spec.Criteria != null)
                {
                    query = query.Where(spec.Criteria);
                }

                query = spec.Includes.Aggregate(query,
                    (current, include) => current.Include(include));

                return query;
            }
    

    }
}
