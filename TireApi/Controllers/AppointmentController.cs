using Microsoft.AspNetCore.Mvc;
using TireApi.Models;
using TireApi.Services.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TireApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }
        // GET: api/<AppointmentController>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _appointmentService.GetAppointmentsAsync();
            return Ok(response);
        }

        // GET api/<AppointmentController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _appointmentService.GetAppointmentByIdAsync(id);
            return Ok(response);
        }

        // POST api/<AppointmentController>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AppointmentModel model)
        {
            var response = await _appointmentService.CreateAppointmentAsync(model);
            return Ok(response);
        }

        // PUT api/<AppointmentController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] AppointmentModel model)
        {
            if(id != model.Id)
            {
                return BadRequest("id not matched");
            }
            var response = await _appointmentService.UpdateAppointmentAsync(model);
            return Ok(response);
        }

        // DELETE api/<AppointmentController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _appointmentService.DeleteAppointmentAsync(id);
            return Ok(response);
        }
    }
}
