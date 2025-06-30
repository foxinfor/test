using DAL.Models;

namespace DAL.Interfaces
{
    public interface ICarRepository : IRepository<Car>
    {
        Task<IEnumerable<Car>> GetAllCarsFilteredAsync(string? Brand, string? Model, int? dailyRate, int? hourlyRate, CancellationToken cancellationToken = default);
        Task<IEnumerable<Car>> GetAllCarsByCharacteristicAsync(string? fuelType, string? transmission, int? seats, string? color, CancellationToken cancellationToken = default);

    }
}
