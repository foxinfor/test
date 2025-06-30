using DAL.Models;

namespace BLL.Interfaces
{
    public interface IPdfService
    {
        Task<byte[]> GenerateRentalAgreementAsync(Booking booking, CancellationToken cancellationToken = default);
    }
}

