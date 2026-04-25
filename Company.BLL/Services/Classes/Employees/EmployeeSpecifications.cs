using Company.BLL.Services.Classes.Specification;
using Company.BLL.Services.Interfaces.Specification;
using Company.DAL.Entities;
using Company.Shared.Dtos.Employees;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Company.BLL.Services.Classes.Employees
{
    public class EmployeeSpecifications : 
        BaseSpecification<int, Employee>
    {


        public EmployeeSpecifications(EmployeeSpecsParams specs) 
            : base((x =>
                    (string.IsNullOrEmpty(specs.Search)) || (x.FullName.ToLower().Contains(specs.Search.ToLower()))))
        {
            Includes.Add(D => D.Department);
        }
    }

}
