using Microsoft.AspNetCore.Identity;
using POS.Application.Enums;

namespace POS.Infrastructure.Seeds
{
	public static class DefaultRoles
	{
		public static async Task SeedAsync(RoleManager<IdentityRole> roleManager)
		{
			await roleManager.CreateAsync(new IdentityRole(Roles.BASIC.ToString()));
			await roleManager.CreateAsync(new IdentityRole(Roles.ADMIN.ToString()));
		}
	}
}
