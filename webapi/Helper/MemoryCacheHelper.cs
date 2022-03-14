using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using webapi.DTOs;
using webapi.Interfaces;

namespace webapi.Helper
{
	public static class MemoryCacheHelper
	{
		public static string NewsDto { get; set; } = "NewsDto";

		private static MemoryCacheEntryOptions GetOptions()
        {
			return new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(30));
		}

		public static async Task<T> GetOrSetCache<T>(string key, IMemoryCache memoryCache, Func<Task<T>> func)
        {
			if (!memoryCache.TryGetValue(key, out T cacheValue))
			{
				var result = await func();

				cacheValue = result;

				memoryCache.Set(key, cacheValue, GetOptions());
			}

			return cacheValue;

		}
	}
}

