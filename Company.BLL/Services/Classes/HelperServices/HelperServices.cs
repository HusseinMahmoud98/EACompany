using Company.BLL.Services.Interfaces.HelperServices;
using Company.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.BLL.Services.Classes.HelperServices
{
    public class HelperServices : IHelperServices
    {
        public byte[] ExportEmployeesToCsv(IEnumerable<Employee> employees)
        {
            var sb = new StringBuilder();

            // Header row
            sb.AppendLine("NationalId,Name,Department,EmpJob");

            // Data rows
            foreach (var emp in employees)
            {
                sb.AppendLine($"{emp.NationalId},{emp.FullName},{emp.Department?.Name ?? "N/A"}, {emp.EmpJob}");
            }

            // Convert to byte array for file download
            return Encoding.UTF8.GetBytes(sb.ToString());
        }

        public IEnumerable<Employee> ImportEmployeesFromCsv(Stream fileStream)
        {
            var employees = new List<Employee>();

            using (var reader = new StreamReader(fileStream))
            {
                string? headerLine = reader.ReadLine(); // skip header

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (string.IsNullOrWhiteSpace(line)) continue;

                    var values = line.Split(',');

                    var employee = new Employee
                    {
                        NationalId = values[0],
                        FullName = values[1],
                        Department = new Department { Name = values[2] },
                        EmpJob = values[3]
                    };

                    employees.Add(employee);
                }
            }

            return employees;
        }
    }
}
