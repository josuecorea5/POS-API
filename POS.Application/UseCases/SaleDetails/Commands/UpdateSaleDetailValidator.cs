using FluentValidation;

namespace POS.Application.UseCases.SaleDetails.Commands
{
	public class UpdateSaleDetailValidator : AbstractValidator<UpdateSaleDetailCommand>
	{
		public UpdateSaleDetailValidator()
		{
			RuleFor(sd => sd.Id).NotEmpty().GreaterThan(0);
			RuleFor(sd => sd.SaleId).NotEmpty().GreaterThan(0);
			RuleFor(sd => sd.ProductId).NotEmpty().GreaterThan(0);
			RuleFor(sd => sd.Quantity).NotEmpty().GreaterThan(0);
			RuleFor(sd => sd.UnitPrice).NotEmpty().GreaterThan(0);
		}
	}
}
