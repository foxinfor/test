using DAL.Interfaces;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class CarsRepository : Repository<Car>, ICarRepository
    {
        public CarsRepository(ApplicationContext context) : base(context) { }

        public async Task<IEnumerable<Car>> GetAllCarsByCharacteristicAsync(string? fuelType, string? transmission, int? seats, string? color, CancellationToken cancellationToken = default)
        {
            var carQuery = _context.Cars.AsQueryable();

            if (!string.IsNullOrWhiteSpace(fuelType))
                carQuery = carQuery.Where(c => c.FuelType == fuelType);

            if (!string.IsNullOrWhiteSpace(transmission))
                carQuery = carQuery.Where(c => c.Transmission == transmission);

            if (seats.HasValue)
                carQuery = carQuery.Where(c => c.Seats <= seats.Value);

            if (!string.IsNullOrWhiteSpace(color))
                carQuery = carQuery.Where(c => c.Color == color);

            return await carQuery.ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Car>> GetAllCarsFilteredAsync(string? brand, string? model, int? dailyRate, int? hourlyRate, CancellationToken cancellationToken = default)
        {
            var carQuery = _context.Cars
                .Where(c => c.IsAvailable)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(brand))
                carQuery = carQuery.Where(c => c.Brand == brand);

            if (!string.IsNullOrWhiteSpace(model))
                carQuery = carQuery.Where(c => c.Model == model);

            if (dailyRate.HasValue)
                carQuery = carQuery.Where(c => c.DailyRate <= dailyRate.Value);

            if (hourlyRate.HasValue)
                carQuery = carQuery.Where(c => c.HourlyRate <= hourlyRate.Value);

            return await carQuery.ToListAsync(cancellationToken);
        }
    }
}
