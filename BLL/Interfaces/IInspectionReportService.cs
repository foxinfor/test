using BLL.DTO.Models;
using BLL.DTO.Requests;

namespace BLL.Interfaces
{
    public interface IInspectionReportService
    {
        Task<IEnumerable<InspectionReportDTO>> GetAllInspectionReportAsync(CancellationToken cancellationToken = default);
        Task<InspectionReportDTO> GetInspectionReportByIdAsync(string id, CancellationToken cancellationToken = default);
        Task<InspectionReportDTO> CreateInspectionReportAsync(CreateInspectionReportDTO createInspectionReportDto, string inspectorId, CancellationToken cancellationToken = default);
        Task UpdateInspectionReportAsync(UpdateInspectionReportDTO updateInspectionReportDto, CancellationToken cancellationToken = default);
        Task DeleteInspectionReportAsync(string id, CancellationToken cancellationToken = default);

        Task<int> CalculateTotalRevenueAsync(CancellationToken cancellationToken = default);

    }
}
