using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.DAL.Entities
{
    public class Employee : BaseEntity<int>
    {
    public string EmpJob { get; set; } = default!;
    public string NationalId { get; set; } = default!;
    public string FullName { get; set; } = default!;

    #region Relationships
    public Department? Department { get; set; } = default!;
    public int? DepartmentId { get; set; }
    #endregion
    }
}
