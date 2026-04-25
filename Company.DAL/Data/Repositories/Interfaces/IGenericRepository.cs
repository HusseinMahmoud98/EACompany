using Company.BLL.Services.Interfaces.Specification;
using Company.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.DAL.Data.Repositories.Interfaces
{
    public interface IGenericRepository<TKey, TEntity> where TEntity : BaseEntity<TKey>
    {
        public int Add(TEntity entity);
        public IQueryable<TEntity> GetAll(ISpecification<TKey,TEntity> specs);
        public Task<TEntity?> GetByIdAsync(TKey id);
        public int Update(TEntity entity);
        public int Delete(TEntity entity);
    }
}
