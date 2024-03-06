using FluentValidation;

namespace POS.Application.UseCases.Products.Commands
{
	public class UpdateProductValidator : AbstractValidator<UpdateProductCommand>
	{
		public UpdateProductValidator()
		{
			RuleFor(p => p.Name).NotEmpty().NotNull().MinimumLength(5);
			RuleFor(p => p.UnitPrice).NotEmpty().NotNull().GreaterThan(0);
			RuleFor(p => p.Status).NotEmpty().IsInEnum();
		}
	}
}
