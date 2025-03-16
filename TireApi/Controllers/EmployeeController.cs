using Microsoft.AspNetCore.Mvc;
using TireApi.Models;
using TireApi.Services.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TireApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _employeeService.GetEmployeesAsync();
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _employeeService.GetEmployeeByIdAsync(id);
            return Ok(response);
        }
       
        // POST api/<EmployeeController>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] EmployeeModel employeeModel)
        {
            var response = await _employeeService.CreateEmployeeAsync(employeeModel);
            return Ok(response);
        }

        // PUT api/<EmployeeController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] EmployeeModel employeeModel)
        {
            if(id != employeeModel.Id)
            {
                return BadRequest("id not match");
            }
            var response = await _employeeService.UpdateEmployeeAsync(employeeModel);
            return Ok(response);
        }

        // DELETE api/<EmployeeController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _employeeService.DeleteEmployeeAsync(id);
            return Ok(response);
        }
    }
}
