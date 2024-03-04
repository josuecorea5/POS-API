using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using POS.Application.Common.Behaviours;
using System.Reflection;

namespace POS.Application
{
	public static class ConfigureServices
	{
		public static IServiceCollection AddApplicationService(this IServiceCollection services)
		{
			services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
			services.AddMediatR(configuration =>
			{
				configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
				configuration.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
			});
			services.AddAutoMapper(Assembly.GetExecutingAssembly());

			return services;
		}
	}
}
