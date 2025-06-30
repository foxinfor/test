using BLL.DTO.Requests;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Test.Controllers
{
    [Route("api/cars")]
    [ApiController]
    public class CarController : ControllerBase 
    {
        private readonly ICarService _carService;

        public CarController(ICarService carService)
        {
            _carService = carService;
        }

        //[Authorize(Policy = "AuthenticatedUsers")]
        [HttpGet("all")]
        public async Task<IActionResult> GetAllCars(CancellationToken cancellationToken = default)
        {
            var cars = await _carService.GetAllCarAsync(cancellationToken);
            return Ok(cars);
        }



        //[Authorize(Policy = "AuthenticatedUsers")]
        [HttpPost]
        public async Task<IActionResult> CreateCar([FromForm] CreateCarDTO createCarDto, CancellationToken cancellationToken = default)
        {
            var car = await _carService.CreateCarAsync(createCarDto, cancellationToken);
            return CreatedAtAction(nameof(GetCarById), new { id = car.Id }, car);
        }


        //[Authorize(Policy = "AuthenticatedUsers")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCarById(string id, CancellationToken cancellationToken = default)
        {
            var car = await _carService.GetCarByIdAsync(id, cancellationToken);
            return Ok(car);
        }


        //[Authorize(Policy = "AuthenticatedUsers")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCar([FromForm] UpdateCarDTO updateCarDTO, CancellationToken cancellationToken = default)
        {
            await _carService.UpdateCarAsync(updateCarDTO, cancellationToken);
            return NoContent();
        }


        //[Authorize(Policy = "OnlyAdminUsers")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCar(string id, CancellationToken cancellationToken = default)
        {
            await _carService.DeleteCarAsync(id, cancellationToken);
            return NoContent();
        }

        //[Authorize(Policy = "OnlyAuthenticatesUsers")]
        [HttpGet("filtered")]
        public async Task<IActionResult> GetAllCarsFiltered([FromQuery] string? brand, [FromQuery] string? model, [FromQuery] int? dailyRate, 
            [FromQuery] int? hourlyRate, CancellationToken cancellationToken = default)
        {
            var cars = await _carService.GetAllCarsFilteredAsync(brand, model, dailyRate, hourlyRate, cancellationToken);
            return Ok(cars);
        }


        //[Authorize(Policy = "OnlyAuthenticatesUsers")]
        [HttpGet("characteristic")]
        public async Task<IActionResult> GetAllCarsByCharacteristic([FromQuery] string? fuelType, [FromQuery] string? transmission, [FromQuery] int? seats,
            [FromQuery] string? color, CancellationToken cancellationToken = default)
        {
            var cars = await _carService.GetAllCarsByCharacteristicAsync(fuelType,transmission,seats,color,cancellationToken);
            return Ok(cars);
        }
    }
}
