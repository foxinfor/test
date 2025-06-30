using BLL.DTO.Models;
using BLL.DTO.Requests;

namespace BLL.Interfaces
{
    public interface IBookingService
    {
        Task<IEnumerable<BookingDTO>> GetAllBookingAsync(CancellationToken cancellationToken = default);
        Task<BookingDTO> GetBookingByIdAsync(string id, CancellationToken cancellationToken = default);
        Task<BookingDTO> CreateBookingAsync(CreateBookingDTO createBookingDto, string userId, CancellationToken cancellationToken = default);
        Task UpdateBookingAsync(UpdateBookingDTO updateBookingDto, CancellationToken cancellationToken = default);
        Task DeleteBookingAsync(string id, CancellationToken cancellationToken = default);


        Task ConfirmBookingAsync(string bookingId, CancellationToken cancellationToken = default);

    }
}
