using POS.App.Modules.GlobalException;

namespace POS.App.Modules.Middleware
{
	public static class MiddlewareExtension
	{
		public static IApplicationBuilder AddMiddleware(this IApplicationBuilder app)
		{
			return app.UseMiddleware<GlobalExceptionHandler>();
		}
	}
}
