using POS.App.Modules.GlobalException;

namespace POS.App
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddPresentation(this IServiceCollection services)
		{
			services.AddTransient<GlobalExceptionHandler>();

			return services;
		}
	}
}
