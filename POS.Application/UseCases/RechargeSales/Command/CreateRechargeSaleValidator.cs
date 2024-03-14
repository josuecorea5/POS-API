using FluentValidation;

namespace POS.Application.UseCases.RechargeSales.Command
{
	public class CreateRechargeSaleValidator : AbstractValidator<CreateRechargeSaleCommand>
	{
        public CreateRechargeSaleValidator()
        {
            RuleFor(rs => rs.SaleId)
                .NotEmpty()
                .GreaterThan(0);
            RuleFor(rs => rs.Percentage)
                .NotEmpty()
                .NotNull()
                .GreaterThan(0)
                .LessThan(1);
            RuleFor(rs => rs.Description)
                .NotEmpty()
                .MaximumLength(200);
            RuleFor(rs => rs.LimitDate)
                .NotEmpty()
                .Must(limitDate => limitDate > DateTime.UtcNow);
        }
    }
}
