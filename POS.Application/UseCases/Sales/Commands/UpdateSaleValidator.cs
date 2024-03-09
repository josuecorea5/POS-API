using FluentValidation;

namespace POS.Application.UseCases.Sales.Commands
{
	public class UpdateSaleValidator : AbstractValidator<UpdateSaleCommand>
	{
        public UpdateSaleValidator()
        {
            RuleFor(s => s.ClientId).GreaterThan(0);
            RuleFor(s => s.SaleStatus).IsInEnum();
        }
    }
}
