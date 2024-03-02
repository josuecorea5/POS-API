using FluentValidation;

namespace POS.Application.UseCases.Users.Commands
{
	public class CreateUserValidator : AbstractValidator<CreateUserCommand>
	{
		public CreateUserValidator()
		{
			RuleFor(x => x.Email).NotNull().NotEmpty().EmailAddress();
			RuleFor(x => x.UserName).NotNull().NotEmpty().MinimumLength(5);
			RuleFor(x => x.Password).NotNull().NotEmpty().MinimumLength(8);
		}
	}
}
