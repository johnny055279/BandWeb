using System;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using webapi.Data;
using webapi.Entities;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace webapi.Extensions
{
	public static class IdentityServiceExtension
	{
		public static IServiceCollection AddIdentityServicesExtension(this IServiceCollection services, IConfiguration configuration)
        {
			services.AddIdentityCore<AppUser>(option =>
			{
				option.Password.RequireNonAlphanumeric = false;
			})
			.AddRoles<AppRole>()
			.AddRoleManager<RoleManager<AppRole>>()
			.AddSignInManager<SignInManager<AppUser>>()
			.AddRoleValidator<RoleValidator<AppRole>>()
			.AddEntityFrameworkStores<DataContext>();

			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(option =>
			{
				option.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuerSigningKey = true,

					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"])),
					// API
					ValidateIssuer = false,
					// Angular
					ValidateAudience = false,
					// 驗證 Token 的有效期間
					ValidateLifetime = true
				};
			});

			services.AddAuthorization(option =>
			{
				option.AddPolicy("RequireAdminRole", policy => policy.RequireRole("Admin"));
			});

			return services;

        }
	}
}

