using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using webapi.DTOs;
using webapi.Entities;

namespace webapi.Interfaces
{
	public interface INewsRepository
	{
		Task<IEnumerable<News>> GetNewsAsync();

		Task<News> GetNewsByIdAsync(int id);

		Task CreateNewsAsync(NewsDto newsDto);

		void UpdateNews(News news);

		void DeleteNews(News news);

	}
}

