using Company.BLL.Services.Interfaces.Specification;
using Company.DAL.Data.Contexts;
using Company.DAL.Data.Repositories.Interfaces;
using Company.DAL.Entities;
using Company.DAL.Specifications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.DAL.Data.Repositories.Classes
{
    public class GenericRepository<TKey, TEntity>(AppDbContext _appDbContext) : IGenericRepository<TKey, TEntity> where TEntity : BaseEntity<TKey>
    {
        public int Add(TEntity entity)
        {
            _appDbContext.Set<TEntity>().Add(entity);
            return _appDbContext.SaveChanges();
        }

        public int Delete(TEntity entity)
        {
            _appDbContext.Remove(entity);
            return _appDbContext.SaveChanges();
        }

        public IQueryable<TEntity> GetAll(ISpecification<TKey, TEntity> specs)
        {
            //return _appDbContext.Set<TEntity>().AsQueryable();
            return SpecificationEvaluator<TKey, TEntity>
                .GetQuery(_appDbContext.Set<TEntity>(), specs);
        }

        public async Task<TEntity?> GetByIdAsync(TKey id)
        {
            return await _appDbContext.Set<TEntity>().FindAsync(id);
        }

        public int Update(TEntity entity)
        {
            _appDbContext.Update(entity);
            return _appDbContext.SaveChanges();
        }
    }
}
