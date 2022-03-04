using System;
using Microsoft.Extensions.DependencyInjection;
using webapi.Filters;

namespace webapi.Extensions
{
	public static class AppFilterExtensions
	{
		public static IServiceCollection AddAppFilterExtensions(this IServiceCollection services)
		{
			services.AddScoped<LogUserActivityFilter>();

			return services;
		}
	}
}

