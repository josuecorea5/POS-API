using FluentValidation;

namespace POS.Application.UseCases.RechargeSales.Command
{
	public class CreateNewRechargeSaleValidator : AbstractValidator<CreateNewRechargeSaleCommand>
	{
        public CreateNewRechargeSaleValidator()
        {
            RuleFor(rs => rs.RechargeId).NotEmpty().GreaterThan(0);
            RuleFor(rs => rs.Description).NotEmpty().MaximumLength(200);
            RuleFor(rs => rs.LimitDate).NotEmpty().Must(limitDate => limitDate > DateTime.UtcNow);
        }
    }
}
