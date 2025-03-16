using Microsoft.AspNetCore.Mvc;
using TireApi.Models;
using TireApi.Services.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TireApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceTypeController : ControllerBase
    {
        private readonly IServiceTypeService _serviceTypeService;
        public ServiceTypeController(IServiceTypeService serviceTypeService)
        {
            _serviceTypeService = serviceTypeService;
        }
        // GET: api/<ServiceTypeController>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _serviceTypeService.GetServiceTypesAsync();
            return Ok(response);
        }

        // GET api/<ServiceTypeController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _serviceTypeService.GetServiceTypeByIdAsync(id);
            return Ok(response);
        }

        // POST api/<ServiceTypeController>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ServiceTypeModel model)
        {
            var response = await _serviceTypeService.CreateServiceTypeAsync(model);
            return Ok(response);
        }

        // PUT api/<ServiceTypeController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ServiceTypeModel model)
        {
            if(id != model.Id)
            {
                return BadRequest("id not matched");
            }
            var response = await _serviceTypeService.UpdateServiceTypeAsync(model);
            return Ok(response);
        }

        // DELETE api/<ServiceTypeController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _serviceTypeService.DeleteServiceTypeAsync(id);
            return Ok(response);
        }
    }
}
