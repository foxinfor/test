using AutoMapper;
using BLL.DTO.Models;
using BLL.DTO.Requests;
using BLL.Exceptions;
using BLL.Interfaces;
using DAL.Interfaces;
using DAL.Models;
using FluentValidation;

namespace BLL.Services
{
    public class CarService : ICarService
    {
        private readonly ICarRepository _carRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<CarDTO> _carDTOValidator;
        private readonly IValidator<CreateCarDTO> _createCarDTOValidator;
        private readonly IValidator<UpdateCarDTO> _updateCarDTOValidator;

        public CarService(ICarRepository carRepository, IMapper mapper, IValidator<CarDTO> carDTOValidator, 
            IValidator<CreateCarDTO> createCarDTOValidator, IValidator<UpdateCarDTO> updateCarDTOValidator)
        {
            _carRepository = carRepository;
            _mapper = mapper;
            _carDTOValidator = carDTOValidator;
            _createCarDTOValidator = createCarDTOValidator;
            _updateCarDTOValidator = updateCarDTOValidator;
        }

        public async Task<CarDTO> CreateCarAsync(CreateCarDTO createCarDto, CancellationToken cancellationToken = default)
        {
            await _createCarDTOValidator.ValidateAndThrowAsync(createCarDto, cancellationToken);

            var car = _mapper.Map<Car>(createCarDto);
            await _carRepository.AddAsync(car, cancellationToken);

            return _mapper.Map<CarDTO>(car);
        }

        public async Task DeleteCarAsync(string id, CancellationToken cancellationToken = default)
        {
            var car = await _carRepository.GetByIdAsync(id, cancellationToken);
            if (car == null)
            {
                throw new NotFoundException("Машина не найдена.");
            }

            await _carRepository.DeleteAsync(car, cancellationToken);
        }

        public async Task<IEnumerable<CarDTO>> GetAllCarAsync(CancellationToken cancellationToken = default)
        {
            var cars = await _carRepository.GetAllAsync(cancellationToken);
            var carDTO = _mapper.Map<IEnumerable<CarDTO>>(cars);
            return carDTO;
        }

        public async Task<CarDTO> GetCarByIdAsync(string id, CancellationToken cancellationToken = default)
        {
            var car = await _carRepository.GetByIdAsync(id, cancellationToken);

            if (car == null)
            {
                throw new NotFoundException("Машина не найдена.");
            }

            var carDTO = _mapper.Map<CarDTO>(car);
            return carDTO;
        }

        public async Task UpdateCarAsync(UpdateCarDTO updateCarDto, CancellationToken cancellationToken = default)
        {
            await _updateCarDTOValidator.ValidateAndThrowAsync(updateCarDto, cancellationToken);

            var existingCar = await _carRepository.GetByIdAsync(updateCarDto.Id, cancellationToken);
            if (existingCar == null)
            {
                throw new NotFoundException($"Бронь с ID {updateCarDto.Id} не найдена.");
            }

            _mapper.Map(updateCarDto, existingCar);
            await _carRepository.UpdateAsync(existingCar, cancellationToken);
        }
    }
}
