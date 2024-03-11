using FluentValidation;

namespace POS.Application.UseCases.SchedulePayments.Commands
{
	public class CreateSchedulePaymentValidator : AbstractValidator<CreateSchedulePaymentCommand>
	{
        public CreateSchedulePaymentValidator()
        {
            RuleFor(sp => sp.SaleId)
                .NotEmpty()
                .GreaterThan(0);
            RuleFor(sp => sp.InitialAmount)
                .GreaterThanOrEqualTo(0);
            RuleFor(sp => sp.LimitDate)
                .NotEmpty()
                .Must(limitDate => limitDate > DateTime.UtcNow).WithMessage("Limit date must not be earlier than the current date");
        }
    }
}
