using FluentValidation;

namespace POS.Application.UseCases.RechargeSales.Command
{
	public class UpdateRechargeSaleStatusValidator : AbstractValidator<UpdateRechargeSaleStatusCommand>
	{
        public UpdateRechargeSaleStatusValidator()
        {
            RuleFor(rs => rs.Id)
                .NotEmpty()
                .GreaterThan(0);

            RuleFor(rs => rs.RechargeSaleStatus)
                .NotEmpty()
                .IsInEnum();
        }
    }
}
