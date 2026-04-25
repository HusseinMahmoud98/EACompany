using AutoMapper;
using Company.BLL.Services.Classes.Employees;
using Company.BLL.Services.Interfaces.Employees;
using Company.BLL.Services.Interfaces.HelperServices;
using Company.BLL.Services.Interfaces.Specification;
using Company.DAL.Entities;
using Company.Shared.Dtos.Employees;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Company.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmployeeController
        (IEmployeeManager _employeeManager, IMapper _mapper, IHelperServices _helperServices) 
        : ControllerBase
    {
        [HttpPost("Add")]
        public IActionResult Add(CreateEmployeeDto createEmployeeDto)
        {
            if (createEmployeeDto is not null)
            {
                var employee = _mapper.Map<Employee>(createEmployeeDto);

                var result = _employeeManager.Add(employee);

                if (result > 0)
                {
                    return Ok(createEmployeeDto);
                }
            }

            return BadRequest();
        }

        
        [HttpGet("GetAll")]
        public IActionResult GetAll([FromQuery] EmployeeSpecsParams employeeSpecsParams)
        {
            
            var employess = _employeeManager.GetAll(employeeSpecsParams);
            var detailsEmployeeDto = _mapper.Map<List<DetailsEmployeeDto>>(employess);
            return Ok(detailsEmployeeDto);
        }

        [HttpGet("ExportCsv")]
        public IActionResult ExportCsv([FromQuery] EmployeeSpecsParams employeeSpecsParams)
        {
            //var spec = new EmployeeSpecsParams(employeeSpecsParams);
            var employees = _employeeManager.GetAll(employeeSpecsParams);

            var csvBytes = _helperServices.ExportEmployeesToCsv(employees);

            return File(csvBytes, "text/csv", "EmployeesReport.csv");
        }

        [HttpPost("ImportCsv")]
        public IActionResult ImportCsv(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            using var stream = file.OpenReadStream();
            var employees = _helperServices.ImportEmployeesFromCsv(stream);
            int count = 0;

            // Optionally save to DB via manager/repository
            foreach (var emp in employees)
            {
                _employeeManager.Add(emp);
                count++;
            }

            return Ok(new { Count = count, Message = "Employees imported successfully" });
        }



    }
}
