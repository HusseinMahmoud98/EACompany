using Company.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.BLL.Services.Interfaces.HelperServices
{
    public interface IHelperServices
    {
        IEnumerable<Employee> ImportEmployeesFromCsv(Stream fileStream);
        byte[] ExportEmployeesToCsv(IEnumerable<Employee> employees);

    }
}
