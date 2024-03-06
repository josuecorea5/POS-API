using FluentValidation;

namespace POS.Application.UseCases.Products.Commands
{
	public class CreateProductValidator : AbstractValidator<CreateProductCommand>
	{
		public CreateProductValidator()
		{
			RuleFor(p => p.Name).NotEmpty().NotNull().MinimumLength(5);
			RuleFor(p => p.UnitPrice).NotEmpty().NotNull().GreaterThan(0);
		}
	}
}
