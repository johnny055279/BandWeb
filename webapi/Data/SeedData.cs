using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using webapi.Entities;

namespace webapi.Data
{
	public static class SeedData
	{
		public static async Task CreateSeedData(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
			var roles = new List<AppRole>
			{
				new AppRole { Name = "Admin"},
				new AppRole { Name = "Member"}
			};

            foreach (var role in roles)
            {
				await roleManager.CreateAsync(role);
            }

			var admin = new AppUser
			{
				UserName = "Admin"
			};

			await userManager.CreateAsync(admin, "Pa$$w0rd");

			await userManager.AddToRolesAsync(admin, new string[] { "Admin", "Member" });
        }
	}
}

