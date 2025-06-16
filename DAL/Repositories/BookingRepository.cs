using DAL.Interfaces;
using DAL.Models;

namespace DAL.Repositories
{
    public class BookingRepository : Repository<Booking>, IBookingRepository
    {
        public BookingRepository(ApplicationContext context) : base(context) { }
    }
}
