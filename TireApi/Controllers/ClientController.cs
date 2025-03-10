using Microsoft.AspNetCore.Mvc;
using TireApi.Models;
using TireApi.Services.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TireApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var response = await _clientService.GetClientsAsync();
            return Ok(response);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Create([FromBody] ClientModel model)
        {
            var response = await _clientService.CreateClientAsync(model);
            return Ok(response);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ClientModel model)
        {
            if(id != model.Id)
            {
                return BadRequest("ID в пути и теле запроса не совпадают!");
            }
            var response = await _clientService.UpdateClientAsync(model);
            return Ok(response);
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _clientService.DeleteClientAsync(id);
            return Ok(response);
        }
    }
}
