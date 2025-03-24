using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Mvc;
using TireApi.Models;
using TireApi.Services.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TireApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : BaseController
    {
        private readonly ICarService _carService;

        public CarController(ICarService carService)
        {
            _carService = carService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _carService.GetCarsAsync();
            return CreateResponse(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _carService.GetCarByIdAsync(id);
            return CreateResponse(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CarModel car)
        {
            var response = await _carService.CreateCarAsync(car);
            return CreateResponse(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CarModel car)
        {
            if(id != car.Id)
            {
                return BadRequest("ID в пути и теле запроса не совпадают!");
            }
            var response = await _carService.UpdateCarAsync(car);
            return CreateResponse(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _carService.DeleteCarAsync(id);
            return CreateResponse(response);
        }
    }
}
