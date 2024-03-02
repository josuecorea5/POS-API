using Microsoft.AspNetCore.Identity;
using POS.Application.Enums;

namespace POS.Infrastructure.Seeds
{
	public static class DefaultAdminUser
	{
		public static async Task SeedAsync(UserManager<IdentityUser> userManager)
		{
			var defaultUser = new IdentityUser
			{
				UserName = "admin",
				Email = "admin@admin.com",
			};

			if(userManager.Users.All(u => u.Id != defaultUser.Id))
			{
				var user = await userManager.FindByEmailAsync(defaultUser.Email);
				if(user is null)
				{
					await userManager.CreateAsync(defaultUser, "Admin@10");
					await userManager.AddToRoleAsync(defaultUser, Roles.ADMIN.ToString());
					await userManager.AddToRoleAsync(defaultUser, Roles.BASIC.ToString());
				}
			}
		}
	}
}
