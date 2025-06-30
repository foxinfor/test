using DAL.Interfaces;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class BookingRepository : Repository<Booking>, IBookingRepository
    {
        public BookingRepository(ApplicationContext context) : base(context) { }

        public async Task<bool> IsCarAvailableAsync(string carId, DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default)
        {
            return !await _context.Bookings
                .AnyAsync(b => b.CarId == carId &&
                               b.StartDate < endDate &&
                               b.EndDate > startDate,
                               cancellationToken);
        }

    }
}
