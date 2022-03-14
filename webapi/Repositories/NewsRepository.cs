using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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

        private IWebHostEnvironment hostingEnvironment;

        private IConfiguration configuration;

        public NewsRepository(DataContext dataContext, IMapper mapper, IWebHostEnvironment hostingEnvironment, IConfiguration configuration)
        {
            this.dataContext = dataContext;

            this.mapper = mapper;

            this.hostingEnvironment = hostingEnvironment;

            this.configuration = configuration;
        }

        public async Task CreateNewsAsync(NewsDto newsDto)
        {
            using var stream = newsDto.Image.OpenReadStream();

            string folderPath = $@"{hostingEnvironment.WebRootPath}/images/news";

            Directory.CreateDirectory(folderPath);

            string imageParh = $@"{hostingEnvironment.WebRootPath}/images/news/{newsDto.Image.FileName}";

            using var fileStream = File.Create(imageParh);

            await stream.CopyToAsync(fileStream);

            string domain = configuration["Host"] + newsDto.Image.FileName;

            newsDto.ImageUrl = domain + newsDto.Image.FileName;

            var news = mapper.Map<News>(newsDto);

            await dataContext.News.AddAsync(news);
        }

        public void DeleteNews(News news)
        {
            dataContext.News.Remove(news);
        }

        public async Task<IEnumerable<News>> GetNewsAsync()
        {
            var news = await dataContext.News.OrderByDescending(n => n.PostDate).ToListAsync();

            return mapper.Map<List<News>>(news).AsEnumerable();
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

