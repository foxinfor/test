using BLL.DTO.Models;
using BLL.DTO.Requests;

namespace BLL.Interfaces
{
    public interface ICarService
    {
        Task<IEnumerable<CarDTO>> GetAllCarAsync(CancellationToken cancellationToken = default);
        Task<CarDTO> GetCarByIdAsync(string id, CancellationToken cancellationToken = default);
        Task<CarDTO> CreateCarAsync(CreateCarDTO createCarDto, CancellationToken cancellationToken = default);
        Task UpdateCarAsync(UpdateCarDTO updateCarDto, CancellationToken cancellationToken = default);
        Task DeleteCarAsync(string id, CancellationToken cancellationToken = default);



        Task<IEnumerable<CarDTO>> GetAllCarsFilteredAsync(string? Brand, string? Model, int? dailyRate, int? hourlyRate, CancellationToken cancellationToken = default);
        Task<IEnumerable<CarDTO>> GetAllCarsByCharacteristicAsync(string? fuelType, string? transmission, int? seats, string? color, CancellationToken cancellationToken = default);

    }
}
