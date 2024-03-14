using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;

namespace POS.App.Modules.Swagger
{
	public static class SwaggerExtension
	{
		public static IServiceCollection AddSwagger(this IServiceCollection services)
		{
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo
				{
					Version = "v1",
					Title = "Point of Sell API",
					Description = "This API handles information about Users, Clients, Products, Sales, Detail Sales, Recharge for sales and Schedule payments."
				});

				c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
				{
					Description = "Enter JWT Bearer token",
					In = ParameterLocation.Header,
					Type = SecuritySchemeType.Http,
					Name = "Authorization",
					Scheme = "bearer"
				});

				c.AddSecurityRequirement(new OpenApiSecurityRequirement
				{
					{
						new OpenApiSecurityScheme
						{
							Reference = new OpenApiReference
							{
								Type = ReferenceType.SecurityScheme,
								Id = "Bearer",
							}
						},
						Array.Empty<string>()
					}
				});
			});

			services.AddControllers().AddJsonOptions(options => {
				var enumConverter = new JsonStringEnumConverter();
				options.JsonSerializerOptions.Converters.Add(enumConverter);
			});

			return services;
		}
	}
}
