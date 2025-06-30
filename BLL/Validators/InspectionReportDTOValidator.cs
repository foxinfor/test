using FluentValidation;
using BLL.DTO.Models;

namespace BLL.Validators
{
    public class InspectionReportDTOValidator : AbstractValidator<InspectionReportDTO>
    {
        public InspectionReportDTOValidator()
        {
            RuleFor(x => x.BookingId)
                .NotEmpty().WithMessage("BookingId обязателен.");

            RuleFor(x => x.InspectorId)
                .NotEmpty().WithMessage("InspectorId обязателен.");

            RuleFor(x => x.ReturnDate)
                .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Дата возврата не может быть в будущем.");

            RuleFor(x => x.FuelLevel)
                .InclusiveBetween(0, 100).WithMessage("Уровень топлива должен быть от 0 до 100%.");

            RuleFor(x => x.Mileage)
                .GreaterThanOrEqualTo(0).WithMessage("Пробег не может быть отрицательным.");

            RuleFor(x => x.SeverityDamage)
                .NotEmpty().When(x => !string.IsNullOrWhiteSpace(x.DescriptionDamage))
                .WithMessage("Укажите степень повреждения.");

            RuleFor(x => x.RepairCost)
                .GreaterThanOrEqualTo(0).WithMessage("Стоимость ремонта не может быть отрицательной.");

            RuleFor(x => x.FinalCharge)
                .GreaterThanOrEqualTo(0).WithMessage("Итоговый платёж не может быть отрицательным.");

            RuleFor(x => x.Photos)
                .NotNull().WithMessage("Список фото не может быть null.")
                .Must(p => p.All(path => !string.IsNullOrWhiteSpace(path)))
                .WithMessage("Пути к фото не должны быть пустыми.");
        }
    }
}
