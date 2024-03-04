using FluentValidation;

namespace POS.Application.UseCases.Clients.Commands
{
	public class CreateClientValidator : AbstractValidator<CreateClientCommand>
	{
		public CreateClientValidator()
		{
			RuleFor(c => c.FullName).NotEmpty().NotNull().MinimumLength(5).MaximumLength(100);
		}
	}
}
