using Microsoft.AspNetCore.Identity;
using POS.App;
using POS.App.Modules.Middleware;
using POS.Application;
using POS.Infrastructure;
using POS.Infrastructure.Seeds;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Infrastructure layer
builder.Services.AddInfrastructureService(builder.Configuration);

//Application Service
builder.Services.AddApplicationService();

//Presentation layer
builder.Services.AddPresentation();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
	var services = scope.ServiceProvider;

	try
	{
		var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
		var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

		await DefaultRoles.SeedAsync(roleManager);
		await DefaultAdminUser.SeedAsync(userManager);
	}catch(Exception ex)
	{
		throw;
	}
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.AddMiddleware();

app.Run();
