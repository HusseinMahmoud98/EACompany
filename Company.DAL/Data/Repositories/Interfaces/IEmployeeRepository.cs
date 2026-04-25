using Company.BLL.Services.Interfaces.Specification;
using Company.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.DAL.Data.Repositories.Interfaces
{
    public interface IEmployeeRepository
    {
        public int Add(Employee employee);
        public Task<Employee?> GetByIdAsync(int Id);
        public IQueryable<Employee> GetAll(ISpecification<int ,Employee> specs);
        public int Update(Employee employee);
        public int Delete(Employee employee);
        
    }
}
