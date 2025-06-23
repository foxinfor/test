using BLL.DTO.Models;
using FluentValidation;

namespace BLL.Validators
{
    public class BookingDTOValidator : AbstractValidator<BookingDTO>
    {
        public BookingDTOValidator()
        {
            RuleFor(b => b.Id)
                .NotEmpty().WithMessage("Id не должен быть пустым.");

            RuleFor(b => b.UserId)
                .NotEmpty().WithMessage("UserId не должен быть пустым.");

            RuleFor(b => b.CarId)
                .NotEmpty().WithMessage("CarId не должен быть пустым.");

            RuleFor(b => b.StartDate)
                .NotEqual(default(DateTime)).WithMessage("StartDate должна быть задана.");

            RuleFor(b => b.EndDate)
                .NotEqual(default(DateTime)).WithMessage("EndDate должна быть задана.")
                .GreaterThan(b => b.StartDate).WithMessage("EndDate должна быть позже StartDate.");

            RuleFor(b => b.Status)
                .NotEmpty().WithMessage("Status не должен быть пустым.");

            RuleFor(b => b.PaymentStatus)
                .NotEmpty().WithMessage("PaymentStatus не должен быть пустым.");

            RuleFor(b => b.CreatedAt)
                .NotEqual(default(DateTime)).WithMessage("CreatedAt должна быть задана.");

            RuleFor(b => b.InsuranceType)
                .NotEmpty().WithMessage("InsuranceType не должен быть пустым.");

            RuleFor(b => b.PickupLocation)
                .NotEmpty().WithMessage("PickupLocation не должен быть пустым.");

            RuleFor(b => b.DropoffLocation)
                .NotEmpty().WithMessage("DropoffLocation не должен быть пустым.");

            RuleFor(b => b.Extras)
                .NotNull().WithMessage("Extras не должен быть null.");
        }
    }
}
