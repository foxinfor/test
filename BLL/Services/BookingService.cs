using AutoMapper;
using BLL.DTO.Models;
using BLL.DTO.Requests;
using BLL.Exceptions;
using BLL.Interfaces;
using DAL.Interfaces;
using DAL.Models;
using FluentValidation;
using Raven.Client.Exceptions;

namespace BLL.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly ICarRepository _carRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<BookingDTO> _bookingDTOValidator;
        private readonly IValidator<CreateBookingDTO> _createBookingDTOValidator;
        private readonly IValidator<UpdateBookingDTO> _updateBookingDTOValidator;

        public BookingService(IBookingRepository bookingRepository, IMapper mapper, ICarRepository carRepository,
            IValidator<BookingDTO> bookingDTOValidator, IValidator<CreateBookingDTO> createBookingDTOValidator,
            IValidator<UpdateBookingDTO> updateBookingDTOValidator)
        {
            _bookingRepository = bookingRepository;
            _mapper = mapper;
            _bookingDTOValidator = bookingDTOValidator;
            _createBookingDTOValidator = createBookingDTOValidator;
            _updateBookingDTOValidator = updateBookingDTOValidator;
            _carRepository = carRepository;
        }



        public async Task<IEnumerable<BookingDTO>> GetAllBookingAsync(CancellationToken cancellationToken = default)
        {
            var bookings = await _bookingRepository.GetAllAsync(cancellationToken);
            var bookingDto = _mapper.Map<IEnumerable<BookingDTO>>(bookings);
            return bookingDto;
        }


        public async Task<BookingDTO> GetBookingByIdAsync(string id, CancellationToken cancellationToken = default)
        {
            var booking = await _bookingRepository.GetByIdAsync(id, cancellationToken);

            if (booking == null)
            {
                throw new NotFoundException("Бронь не найдена.");
            }

            var bookingDto = _mapper.Map<BookingDTO>(booking);
            return bookingDto;
        }






        public async Task<BookingDTO> CreateBookingAsync(CreateBookingDTO createBookingDto, string userId, CancellationToken cancellationToken = default)
        {
            var car = await _carRepository.GetByIdAsync(createBookingDto.CarId, cancellationToken);
            if (car == null)
                throw new InvalidOperationException("Автомобиль не найден.");


            var rentalDuration = createBookingDto.RateType.ToLower() switch
            {
                "daily" => (int)Math.Ceiling((createBookingDto.EndDate - createBookingDto.StartDate).TotalDays),
                "hourly" => (int)Math.Ceiling((createBookingDto.EndDate - createBookingDto.StartDate).TotalHours),
                _ => throw new InvalidOperationException("Недопустимый тип тарифа.")
            };

            var rate = createBookingDto.RateType.ToLower() switch
            {
                "daily" => car.DailyRate,
                "hourly" => car.HourlyRate,
                _ => 0
            };

            var totalPrice = rentalDuration * rate;


            var available = await _bookingRepository.IsCarAvailableAsync(createBookingDto.CarId, createBookingDto.StartDate, createBookingDto.EndDate, cancellationToken);

            if (!available)
                throw new InvalidOperationException("Автомобиль недоступен на выбранные даты.");



            await _createBookingDTOValidator.ValidateAndThrowAsync(createBookingDto, cancellationToken);
                

            var booking = _mapper.Map<Booking>(createBookingDto);

            booking.UserId = userId;
            booking.CreatedAt = DateTime.UtcNow;
            booking.Id = Guid.NewGuid().ToString();
            booking.TotalPrice = totalPrice;
            booking.PaymentStatus = "paid";
            booking.Status = "unconfirmed";

            await _bookingRepository.AddAsync(booking, cancellationToken);

            return _mapper.Map<BookingDTO>(booking);
        }

        public async Task ConfirmBookingAsync(string bookingId, CancellationToken cancellationToken = default)
        {
            var booking = await _bookingRepository.GetByIdAsync(bookingId, cancellationToken);

            if (booking == null)
                throw new InvalidOperationException("Бронирование не найдено.");

            booking.Status = "confirmed";

            await _bookingRepository.UpdateAsync(booking, cancellationToken);
        }






        public async Task UpdateBookingAsync(UpdateBookingDTO updateBookingDto, CancellationToken cancellationToken = default)
        {
            await _updateBookingDTOValidator.ValidateAndThrowAsync(updateBookingDto, cancellationToken);

            var existingBooking = await _bookingRepository.GetByIdAsync(updateBookingDto.Id, cancellationToken);
            if (existingBooking == null)
            {
                throw new NotFoundException($"Бронь с ID {updateBookingDto.Id} не найдена.");
            }

            _mapper.Map(updateBookingDto, existingBooking);
            await _bookingRepository.UpdateAsync(existingBooking, cancellationToken);
        }

        public async Task DeleteBookingAsync(string id, CancellationToken cancellationToken = default)
        {
            var booking = await _bookingRepository.GetByIdAsync(id, cancellationToken);
            if (booking == null)
            {
                throw new NotFoundException("Бронирование не найдено.");
            }

            await _bookingRepository.DeleteAsync(booking, cancellationToken);
        }
    }
}
