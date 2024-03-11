using FluentValidation;

namespace POS.Application.UseCases.SchedulePayments.Commands
{
	public class UpdateSchedulePaymentStatusValidator : AbstractValidator<UpdateSchedulePaymentStatusCommand>
	{
        public UpdateSchedulePaymentStatusValidator()
        {
            RuleFor(spStatus => spStatus.Id).NotEmpty().GreaterThan(0);
			RuleFor(spStatus => spStatus.SchedulePaymentStatus).NotEmpty().IsInEnum();
		}
	}
}
