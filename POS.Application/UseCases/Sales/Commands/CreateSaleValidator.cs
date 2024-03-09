using FluentValidation;
using POS.Application.UseCases.SaleDetails.Commands;

namespace POS.Application.UseCases.Sales.Commands
{
	public class CreateSaleValidator : AbstractValidator<CreateSaleCommand>
	{
		public CreateSaleValidator()
		{
			RuleFor(s => s.SaleStatus).NotEmpty().NotNull().IsInEnum();
			RuleFor(s => s.ClientId).NotEmpty().NotNull().GreaterThan(0);
			RuleForEach(s => s.SaleDetails).SetValidator(new CreateSaleDetailValidator());
		}
	}
}
