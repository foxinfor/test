using BLL.DTO.Requests;
using FluentValidation;

namespace BLL.Validators
{
    public class CreateCarDTOValidator : AbstractValidator<CreateCarDTO>
    {
        public CreateCarDTOValidator() 
        {
            RuleFor(c => c.Brand)
                .NotEmpty().WithMessage("Brand не должен быть пустым.");

            RuleFor(c => c.Model)
                .NotEmpty().WithMessage("Model не должен быть пустым.");

            RuleFor(c => c.Year)
                .GreaterThan(1900).WithMessage("Year должен быть больше 1900.");

            RuleFor(c => c.LicensePlate)
                .NotEmpty().WithMessage("LicensePlate не должен быть пустым.");

            RuleFor(c => c.Vin)
                .NotEmpty().WithMessage("VIN не должен быть пустым.");

            RuleFor(c => c.FuelType)
                .NotEmpty().WithMessage("FuelType не должен быть пустым.");

            RuleFor(c => c.Transmission)
                .NotEmpty().WithMessage("Transmission не должна быть пустой.");

            RuleFor(c => c.Seats)
                .GreaterThan(0).WithMessage("Seats должен быть больше 0.");

            RuleFor(c => c.Color)
                .NotEmpty().WithMessage("Color не должен быть пустым.");

            RuleFor(c => c.DailyRate)
                .GreaterThan(0).WithMessage("DailyRate должен быть положительным числом.");

            RuleFor(c => c.HourlyRate)
                .GreaterThan(0).WithMessage("HourlyRate должен быть положительным числом.");

            RuleFor(c => c.Location)
                .NotEmpty().WithMessage("Location не должен быть пустым.");

            RuleFor(c => c.Features)
                .NotNull().WithMessage("Features не должен быть null.");

            RuleFor(c => c.Images)
                .NotNull().WithMessage("Images не должен быть null.");
        }
    }
}
