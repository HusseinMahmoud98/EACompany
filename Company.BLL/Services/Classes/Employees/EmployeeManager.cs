using Company.BLL.Services.Interfaces.Employees;
using Company.BLL.Services.Interfaces.Specification;
using Company.DAL.Data.Repositories.Interfaces;
using Company.DAL.Entities;
using Company.Shared.Dtos.Employees;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.BLL.Services.Classes.Employees
{
    public class EmployeeManager(IEmployeeRepository _employeeRepository) : IEmployeeManager
    {
        public int Add(Employee employee)
        {
            return _employeeRepository.Add(employee);
        }
        public void Delete(Employee employee)
        {
            _employeeRepository.Delete(employee);
        }

        public IEnumerable<Employee> GetAll(EmployeeSpecsParams employeeSpecsParams)
        {
            var specs = new EmployeeSpecifications(employeeSpecsParams);
            return _employeeRepository.GetAll(specs).AsEnumerable();
        }

        public async Task<Employee?> GetByIdAsync(int id)
        {
            return await _employeeRepository.GetByIdAsync(id);
        }

        public void Update(Employee employee)
        {
            _employeeRepository.Update(employee);
        }
    }
}
