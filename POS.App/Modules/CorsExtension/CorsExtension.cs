namespace POS.App.Modules.CorsExtension
{
	public static class CorsExtension
	{
		public static IServiceCollection AddCorsExtension(this IServiceCollection services, IConfiguration configuration)
		{
			var policy = "policyPOS";
			services.AddCors(options =>
			{
				options.AddPolicy(policy, policy => policy.WithOrigins(configuration["Config:OriginCors"])
								  .AllowAnyHeader()
								  .AllowAnyMethod());
			});

			return services;
		}
	}
}
