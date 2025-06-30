using FluentValidation;
using BLL.DTO.Requests;

namespace BLL.Validators
{
    public class CreateInspectionReportDTOValidator : AbstractValidator<CreateInspectionReportDTO>
    {
        public CreateInspectionReportDTOValidator()
        {
            RuleFor(x => x.BookingId)
                .NotEmpty().WithMessage("BookingId обязателен.");

            RuleFor(x => x.ReturnDate)
                .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Дата возврата не может быть в будущем.");

            RuleFor(x => x.FuelLevel)
                .InclusiveBetween(0, 100).WithMessage("Уровень топлива должен быть от 0 до 100%.");

            RuleFor(x => x.Mileage)
                .GreaterThanOrEqualTo(0).WithMessage("Пробег не может быть отрицательным.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Описание повреждений обязательно.")
                .MaximumLength(500).WithMessage("Описание не должно превышать 500 символов.");

            RuleFor(x => x.Severity)
                .NotEmpty().WithMessage("Уровень повреждений обязателен.")
                .Matches("(?i)^(Minor|Medium|High)$")
                .WithMessage("Допустимые значения: Minor, Medium, High.");

            RuleFor(x => x.RepairCost)
                .GreaterThanOrEqualTo(0).WithMessage("Стоимость ремонта не может быть отрицательной.");

            RuleFor(x => x.Notes)
                .MaximumLength(1000).WithMessage("Примечание не должно превышать 1000 символов.");

            RuleForEach(x => x.Photos)
                .NotEmpty().WithMessage("Фото не должно быть пустым.")
                .Must(p => p.EndsWith(".jpg") || p.EndsWith(".png") || p.EndsWith(".jpeg"))
                .WithMessage("Допустимые форматы изображений: .jpg, .jpeg, .png");
        }
    }
}
