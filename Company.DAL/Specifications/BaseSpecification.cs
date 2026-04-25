using Company.BLL.Services.Interfaces.Specification;
using Company.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Company.BLL.Services.Classes.Specification
{
    public class BaseSpecification<TKey, TEntity> : ISpecification<TKey, TEntity> where TEntity : BaseEntity<TKey>
    {
        public List<Expression<Func<TEntity, object>>> Includes { get; set; } = new List<Expression<Func<TEntity, object>>>();
        public Expression<Func<TEntity, bool>>? Criteria { get; set; }

        public BaseSpecification(Expression<Func<TEntity, bool>>? expression)
        {
            Criteria = expression;
        }
    }
}
