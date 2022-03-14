using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using webapi.DTOs;
using webapi.Entities;
using webapi.Helper;
using webapi.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace webapi.Controllers
{
    public class NewsController : BaseController
    {
        private readonly IMapper mapper;

        private readonly IUnitOfWork unitOfWork;

        private readonly IMemoryCache memoryCache;

        public NewsController(IMapper mapper, IUnitOfWork unitOfWork, IMemoryCache memoryCache)
        {
            this.mapper = mapper;

            this.unitOfWork = unitOfWork;

            this.memoryCache = memoryCache;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<NewsDto>>> GetNewsAsync()
        {
            var news = await MemoryCacheHelper.GetOrSetCache(MemoryCacheHelper.NewsDto, memoryCache, unitOfWork.NewsRepository.GetNewsAsync);

            return Ok(news);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<NewsDto>> GetNewsByIdAsync(int id)
        {
            var news = await unitOfWork.NewsRepository.GetNewsByIdAsync(id);

            if (news == null) return NotFound("Can't find news");

            return Ok(news);
        }

        [HttpPost]
        public async Task<ActionResult> CreateNewsAsync(NewsDto newsDto)
        {
            if (newsDto.Image.Length > 0 == false) return BadRequest("Can't find image");

            await unitOfWork.NewsRepository.CreateNewsAsync(newsDto);

            if (!await unitOfWork.Complete()) return BadRequest("Create news faild");

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateNews(int id, NewsDto newsDto)
        {
            var news = await unitOfWork.NewsRepository.GetNewsByIdAsync(id);

            mapper.Map(news, newsDto);

            unitOfWork.NewsRepository.UpdateNews(news);

            if (!await unitOfWork.Complete()) return BadRequest("Update news faild");

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteNews(int id)
        {
            var news = await unitOfWork.NewsRepository.GetNewsByIdAsync(id);

            unitOfWork.NewsRepository.DeleteNews(news);

            if (!await unitOfWork.Complete()) return BadRequest("Delete news faild");

            return NoContent();
        }
    }
}

