using FluentValidation;

namespace POS.Application.UseCases.SaleDetails.Commands
{
	public class CreateSaleDetailValidator : AbstractValidator<CreateSaleDetailCommand>
	{
		public CreateSaleDetailValidator()
		{
			RuleFor(sd => sd.ProductId).NotEmpty().NotNull().GreaterThan(0);
			RuleFor(sd => sd.Quantity).NotEmpty().GreaterThan(0);
			RuleFor(sd => sd.UnitPrice).NotEmpty().GreaterThan(0);
			RuleFor(sd => sd.Discount).NotEmpty().GreaterThanOrEqualTo(0);
		}
	}
}
