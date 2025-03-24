using Microsoft.AspNetCore.Mvc;
using TireApi.Models;
using TireApi.Services.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TireApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : BaseController
    {
        private readonly IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _clientService.GetClientsAsync();
            return CreateResponse(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _clientService.GetClientByIdAsync(id);
            return CreateResponse(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ClientModel model)
        {
            var response = await _clientService.CreateClientAsync(model);
            return CreateResponse(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ClientModel model)
        {
            if(id != model.Id)
            {
                return BadRequest("ID в пути и теле запроса не совпадают!");
            }
            var response = await _clientService.UpdateClientAsync(model);
            return CreateResponse(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _clientService.DeleteClientAsync(id);
            return CreateResponse(response);
        }
    }
}
