using Company.BLL.Services.Interfaces.Specification;
using Company.DAL.Data.Repositories.Interfaces;
using Company.DAL.Entities;
using Company.Shared.Dtos.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.BLL.Services.Interfaces.Employees
{
    public interface IEmployeeManager
    {
        public int Add(Employee employee);
        public IEnumerable<Employee> GetAll(EmployeeSpecsParams employeeSpecsParams);
        public Task<Employee?> GetByIdAsync(int id);
        public void Update(Employee employee);
        public void Delete(Employee employee);
    }
}
