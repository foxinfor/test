using DAL.Models;

namespace DAL.Interfaces
{
    public interface IBookingRepository : IRepository<Booking>
    {
        Task<bool> IsCarAvailableAsync(string carId, DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default);

    }
}
