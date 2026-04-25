using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Shared.Dtos.Employees
{
    public class DetailsEmployeeDto
    {
        public string EmpJob { get; set; } = default!;
        public string NationalId { get; set; } = default!;
        public string FullName { get; set; } = default!;
        public int? DepartmentId { get; set; }
    }
}
