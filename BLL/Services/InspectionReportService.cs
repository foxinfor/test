using AutoMapper;
using BLL.DTO.Models;
using BLL.DTO.Requests;
using BLL.Exceptions;
using BLL.Interfaces;
using BLL.Validators;
using DAL.Interfaces;
using DAL.Models;
using DAL.Repositories;
using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace BLL.Services
{
    public class InspectionReportService : IInspectionReportService
    {
        private readonly IInspectionReportRepository _repository;
        private readonly IBookingRepository _bookingRepository;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly IValidator<InspectionReportDTO> _inspectionDTOValidator;
        private readonly IValidator<CreateInspectionReportDTO> _createBookingDTOValidator;
        private readonly IValidator<UpdateInspectionReportDTO> _updateBookingDTOValidator;

        public InspectionReportService(IInspectionReportRepository repository, IMapper mapper, IValidator<InspectionReportDTO> inspectionDTOValidator, 
            IBookingRepository bookingRepository,UserManager<User> usermanager,
            IValidator<CreateInspectionReportDTO> createBookingDTOValidator, IValidator<UpdateInspectionReportDTO> updateBookingDTOValidator)
        {
            _repository = repository;
            _mapper = mapper;
            _inspectionDTOValidator = inspectionDTOValidator;
            _createBookingDTOValidator = createBookingDTOValidator;
            _updateBookingDTOValidator = updateBookingDTOValidator;
            _bookingRepository = bookingRepository;
            _userManager = usermanager;
        }

        public async Task<InspectionReportDTO> CreateInspectionReportAsync(CreateInspectionReportDTO createInspectionReportDto,string inspectorId, CancellationToken cancellationToken = default)
        {
            await _createBookingDTOValidator.ValidateAndThrowAsync(createInspectionReportDto, cancellationToken);

            var report = _mapper.Map<InspectionReport>(createInspectionReportDto);
            report.Id = Guid.NewGuid().ToString();
            report.InspectorId = inspectorId;

            var booking = await _bookingRepository.GetByIdAsync(createInspectionReportDto.BookingId, cancellationToken);
            if (booking == null)
                throw new InvalidOperationException("Бронирование не найдено.");

            report.FinalCharge = booking.TotalPrice + createInspectionReportDto.RepairCost;

            await _repository.AddAsync(report, cancellationToken);

            var user = await _userManager.FindByIdAsync(booking.UserId);
            if (user == null)
                throw new InvalidOperationException("Пользователь не найден.");

            user.LoyaltyPoints = user.LoyaltyPoints - report.FinalCharge;

            await _userManager.UpdateAsync(user);


            return _mapper.Map<InspectionReportDTO>(report);
        }

        public Task DeleteInspectionReportAsync(string id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<InspectionReportDTO>> GetAllInspectionReportAsync(CancellationToken cancellationToken = default)
        {
            var inspectionReports = await _repository.GetAllAsync(cancellationToken);
            var reportDto = _mapper.Map<IEnumerable<InspectionReportDTO>>(inspectionReports);
            return reportDto;
        }

        public async Task<InspectionReportDTO> GetInspectionReportByIdAsync(string id, CancellationToken cancellationToken = default)
        {
            var report = await _repository.GetByIdAsync(id, cancellationToken);

            if (report == null)
            {
                throw new NotFoundException("Отчет о возврате не найден.");
            }

            var reportDto = _mapper.Map<InspectionReportDTO>(report);
            return reportDto;
        }

        public Task UpdateInspectionReportAsync(UpdateInspectionReportDTO updateInspectionReportDto, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<int> CalculateTotalRevenueAsync(CancellationToken cancellationToken = default)
        {
            var allReports = await _repository.GetAllAsync(cancellationToken);
            return allReports.Sum(report => report.FinalCharge);
        }


        public async Task<Dictionary<string, double>> CalculateAverageRatingPerCarAsync(CancellationToken cancellationToken = default)
        {
            var reports = await _repository.GetAllAsync(cancellationToken);

            var results = new Dictionary<string, List<int>>();

            foreach (var report in reports)
            {
                var booking = await _bookingRepository.GetByIdAsync(report.BookingId, cancellationToken);
                if (booking == null)
                    continue;

                var carId = booking.CarId;

                if (!results.ContainsKey(carId))
                {
                    results[carId] = new List<int>();
                }

                results[carId].Add(report.Rating);
            }

            return results.ToDictionary(
                pair => pair.Key,
                pair => pair.Value.Average()
            );
        }
    }
}
