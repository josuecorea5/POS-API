using FluentValidation;
using MediatR;
using POS.Application.Common.Exceptions;

namespace POS.Application.Common.Behaviours
{
	public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
	{
		private readonly IEnumerable<IValidator<TRequest>> _validators;

		public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
		{
			_validators = validators;
		}

		public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
		{
			if(_validators.Any())
			{
				var context = new ValidationContext<TRequest>(request);
				var validationResult = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));
				var failures = validationResult.Where(r => r.Errors.Count != 0).SelectMany(r => r.Errors)
					.Select(r => new BaseError() { PropertyName = r.PropertyName, PropertyMessage = r.ErrorMessage }).ToList();

				if(failures.Count != 0)
				{
					throw new ValidationCustomException(failures);
				}
			}

			return await next();
		}
	}
}
