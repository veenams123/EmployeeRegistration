using EmployeeRegistration.Application.Interface;
using EmployeeRegistration.Core.Entitites.Employee;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeRegitsration.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepo;
        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(IEmployeeRepository employeeRepo,ILogger<EmployeeController> logger)
        {
            _employeeRepo = employeeRepo;
            _logger = logger;
        }
        [HttpGet("search")]        
        public async Task<IActionResult> GetEmployees(string? name, int pageNumber, int pageSize)
        {
            try
            {
                var employees = await _employeeRepo.GetEmployees(name, pageNumber, pageSize);
                return Ok(employees);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> CreateEmployee(Employee employee)
        {
            try
            {
                await _employeeRepo.CreateEmployee(employee);
                return Ok();
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }
    }
}
