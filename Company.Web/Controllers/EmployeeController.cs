using AutoMapper;
using Company.BLL.Services.Interfaces.Employees;
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
    public class EmployeeController(IEmployeeManager _employeeManager, IMapper _mapper) : ControllerBase
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

      

    }
}
