using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using webapi.Data;
using webapi.DTOs;
using webapi.Entities;
using webapi.Interfaces;

namespace webapi.Repositories
{
	public class NewsRepository : INewsRepository
	{
		private readonly DataContext dataContext;

        private readonly IMapper mapper;

        public NewsRepository(DataContext dataContext, IMapper mapper)
        {
            this.dataContext = dataContext;

            this.mapper = mapper;
        }

        public async Task CreateNewsAsync(News news)
        {
            await dataContext.News.AddAsync(news);
        }

        public void DeleteNews(News news)
        {
            dataContext.News.Remove(news);
        }

        public async Task<IEnumerable<News>> GetNewsAsync()
        {
            var news = await dataContext.News.OrderByDescending(n => n.PostDate).ToListAsync();

            return mapper.Map<IEnumerable<News>>(news);
        }

        public async Task<News> GetNewsByIdAsync(int id)
        {
            return await dataContext.News.FindAsync(id);
        }

        public void UpdateNews(News news)
        {
            dataContext.Entry<News>(news).State = EntityState.Modified;
        }
    }
}

