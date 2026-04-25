using Company.DAL.Data.Contexts;
using Company.DAL.Data.Repositories.Interfaces;
using Company.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.DAL.Data.Repositories.Classes
{
    public class EmployeeRepository(AppDbContext _appDbContext)
        : GenericRepository<int, Employee>(_appDbContext), IEmployeeRepository
    {
    }
}
