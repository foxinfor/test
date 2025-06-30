using AutoMapper;
using BLL.DTO.Models;
using BLL.DTO.Requests;
using BLL.Exceptions;
using BLL.Interfaces;
using BLL.Services;
using DAL.Interfaces;
using DAL.Models;
using FluentValidation;
using Moq;

namespace ForTest
{
    public class UnitTest1
    {
        private readonly Mock<IBookingRepository> _bookingRepoMock = new();
        private readonly Mock<ICarRepository> _carRepoMock = new();
        private readonly Mock<IPdfService> _pdfMock = new();
        private readonly IMapper _mapper;
        private readonly Mock<IValidator<CreateBookingDTO>> _createValidatorMock = new();
        private readonly Mock<IValidator<BookingDTO>> _bookingValidatorMock = new();
        private readonly Mock<IValidator<UpdateBookingDTO>> _updateValidatorMock = new();

        public UnitTest1()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CreateBookingDTO, Booking>();
                cfg.CreateMap<Booking, BookingDTO>();
            });
            _mapper = config.CreateMapper();
        }

        [Fact]
        public async Task CreateBookingAsync_ShouldReturnBookingDto_WhenValidInput()
        {
            var bookingDto = new CreateBookingDTO
            {
                CarId = "car123",
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddDays(2),
                RateType = "daily"
            };

            var car = new Car { Id = "car123", DailyRate = 100, HourlyRate = 10 };

            _carRepoMock.Setup(x => x.GetByIdAsync("car123", default)).ReturnsAsync(car);
            _bookingRepoMock.Setup(x => x.IsCarAvailableAsync(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(), default)).ReturnsAsync(true);
            _createValidatorMock.Setup(v => v.ValidateAsync(bookingDto, default)).ReturnsAsync(new FluentValidation.Results.ValidationResult());

            var service = new BookingService(
                _bookingRepoMock.Object,
                _mapper,
                _carRepoMock.Object,
                _pdfMock.Object,
                _bookingValidatorMock.Object,
                _createValidatorMock.Object,
                _updateValidatorMock.Object);

            var result = await service.CreateBookingAsync(bookingDto, "user123");

            Assert.NotNull(result);
            Assert.Equal("user123", result.UserId);
        }

        [Fact]
        public async Task GetBookingByIdAsync_ShouldThrowNotFound_WhenBookingNotFound()
        {
            _bookingRepoMock.Setup(x => x.GetByIdAsync("bad_id", default)).ReturnsAsync((Booking?)null);
            var service = new BookingService(
                _bookingRepoMock.Object,
                _mapper,
                _carRepoMock.Object,
                _pdfMock.Object,
                _bookingValidatorMock.Object,
                _createValidatorMock.Object,
                _updateValidatorMock.Object);

            await Assert.ThrowsAsync<NotFoundException>(() =>
                service.GetBookingByIdAsync("bad_id"));
        }

        [Fact]
        public async Task ConfirmBookingAsync_ShouldUpdateStatus()
        {
            var booking = new Booking { Id = "b1", Status = "unconfirmed" };
            _bookingRepoMock.Setup(x => x.GetByIdAsync("b1", default)).ReturnsAsync(booking);

            var service = new BookingService(
                _bookingRepoMock.Object,
                _mapper,
                _carRepoMock.Object,
                _pdfMock.Object,
                _bookingValidatorMock.Object,
                _createValidatorMock.Object,
                _updateValidatorMock.Object);

            await service.ConfirmBookingAsync("b1");

            Assert.Equal("confirmed", booking.Status);
            _bookingRepoMock.Verify(x => x.UpdateAsync(booking, default), Times.Once);
        }

        [Fact]
        public async Task DeleteBookingAsync_ShouldCallRepositoryDelete()
        {
            var booking = new Booking { Id = "delete1" };
            _bookingRepoMock.Setup(x => x.GetByIdAsync("delete1", default)).ReturnsAsync(booking);

            var service = new BookingService(
                _bookingRepoMock.Object,
                _mapper,
                _carRepoMock.Object,
                _pdfMock.Object,
                _bookingValidatorMock.Object,
                _createValidatorMock.Object,
                _updateValidatorMock.Object);

            await service.DeleteBookingAsync("delete1");

            _bookingRepoMock.Verify(x => x.DeleteAsync(booking, default), Times.Once);
        }
    }
}