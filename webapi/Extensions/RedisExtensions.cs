using System;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace webapi.Extensions
{
	public static class RedisExtensions
	{
		public static IServiceCollection AddStackExchangeRedisExtensions(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(configuration.GetValue<string>("RedisConnection")));

			return services;
		}

		public static async Task<T> GetOrSetAsync<T, Tkey>(this IDatabase database, Tkey anyKey, Func<int, Task<T>> func) where T : class
        {
			var key = anyKey switch
			{
				string k => k,
				_ => anyKey.ToString()
			};

			var jsonResult = await database.StringGetAsync(key);

			if(jsonResult.IsNullOrEmpty)
            {
				var value = await func(int.Parse(key.Split("_")[1]));

				return value;
            }

			return JsonSerializer.Deserialize<T>(jsonResult);
        }
	}
}

