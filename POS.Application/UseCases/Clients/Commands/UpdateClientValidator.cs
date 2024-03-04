using FluentValidation;

namespace POS.Application.UseCases.Clients.Commands
{
	public class UpdateClientValidator : AbstractValidator<UpdateClientCommand>
	{
		public UpdateClientValidator()
		{
			RuleFor(c => c.FullName).NotEmpty().NotNull().MinimumLength(5).MaximumLength(100);
		}
	}
}
