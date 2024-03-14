using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using POS.App.Helpers;
using POS.Application.Common;
using POS.Application.Interfaces;
using POS.Infrastructure.Context;
using POS.Infrastructure.Interceptors;
using POS.Infrastructure.Repositories;
using POS.Infrastructure.Services;
using System.Text;
using System.Text.Json;

namespace POS.Infrastructure
{
	public static class ConfigureServices
	{
		public static IServiceCollection AddInfrastructureService(this IServiceCollection services, IConfiguration configuration)
		{
			services.Configure<AppSettings>(configuration.GetSection("Config"));
			services.AddSingleton<ICurrentUserService, CurrentUserService>();
			services.AddScoped<AuditableEntitySaveChangesInterceptor>();
			services.AddScoped<SoftDeleteInterceptor>();

			services.AddIdentity<IdentityUser, IdentityRole>(options =>
			{
				options.Password.RequiredLength = 8;
				options.User.RequireUniqueEmail = true;
			}).AddEntityFrameworkStores<POSDbContext>().AddDefaultTokenProviders();

			services.AddDbContext<POSDbContext>(options =>
			{
				options.UseSqlServer(configuration.GetConnectionString("POSConnection"),
				builder => builder.MigrationsAssembly(typeof(POSDbContext).Assembly.FullName));
			});

			var key = Encoding.ASCII.GetBytes(configuration["Config:Secret"]);
			var issuer = configuration["Config:Issuer"];
			var audience = configuration["Config:Audience"];

			services.AddAuthentication(x =>
			{
				x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(x =>
			{
				x.Events = new JwtBearerEvents
				{
					OnTokenValidated = context =>
					{
						var userId = context.Principal.Identity.Name;
						return Task.CompletedTask;
					},
					OnAuthenticationFailed = context =>
					{
						if (context.Exception.GetType() == typeof(SecurityTokenValidationException))
						{
							context.Response.StatusCode = 500;
							context.Response.ContentType = "application/json";
							context.Response.Headers.Add("Token-Expired", "true");
						}
						return context.Response.WriteAsync(context.Exception.ToString());
					},
					OnForbidden = context =>
					{
						context.Response.StatusCode = StatusCodes.Status401Unauthorized;
						context.Response.ContentType = "application/json";
						var result = JsonSerializer.Serialize(new Response<string> { Message = "Unauthorized" });
						return context.Response.WriteAsync(result);
					},
					OnChallenge = context =>
					{
						context.HandleResponse();
						context.Response.StatusCode = StatusCodes.Status400BadRequest;
						context.Response.ContentType = "application/json";
						var result = JsonSerializer.Serialize(new Response<string> { Message = "Unauthorized" });
						return context.Response.WriteAsync(result);
					}
				};
				x.RequireHttpsMetadata = false;
				x.SaveToken = false;
				x.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(key),
					ValidateIssuer = true,
					ValidIssuer = issuer,
					ValidateAudience = true,
					ValidAudience = audience,
					ValidateLifetime = true,
					ClockSkew = TimeSpan.Zero
				};
			});

			services.AddScoped<IAuthService, AuthService>();
			services.AddScoped<IUnitOfWork, UnitOfWork>();
			services.AddScoped<IClientRepository, ClientRepository>();
			services.AddScoped<IProductRepository, ProductRepository>();
			services.AddScoped<ISaleRepository, SaleRepository>();
			services.AddScoped<ISaleDetailRepository, SaleDetailRepository>();
			services.AddScoped<ISchedulePaymentRepository, SchedulePaymentRespository>();
			services.AddScoped<IRechargeSaleRepository, RechargeSaleRepository>();

			return services;
		}
	}
}
