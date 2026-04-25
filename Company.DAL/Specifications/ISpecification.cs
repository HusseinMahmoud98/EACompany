using Company.DAL.Entities;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Company.BLL.Services.Interfaces.Specification
{
    public interface ISpecification<TKey, TEntity> where TEntity : BaseEntity<TKey>
    {
        public List<Expression<Func<TEntity, object>>> Includes { get; }
        public Expression<Func<TEntity, bool>>? Criteria { get; }

    }
}
