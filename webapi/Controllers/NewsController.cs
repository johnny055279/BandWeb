using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using webapi.DTOs;
using webapi.Entities;
using webapi.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace webapi.Controllers
{
    public class NewsController : Controller
    {
        private readonly IMapper mapper;

        private readonly IUnitOfWork unitOfWork;

        public NewsController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;

            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<NewsDto>>> GetNewsAsync()
        {
            var news = await unitOfWork.NewsRepository.GetNewsAsync();

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
            var news = mapper.Map<News>(newsDto);

            await unitOfWork.NewsRepository.CreateNewsAsync(news);

            if (!await unitOfWork.Complete()) return BadRequest("Create news faild");

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateNews(int id, NewsDto newsDto)
        {
            var news = await unitOfWork.NewsRepository.GetNewsByIdAsync(id);

            mapper.Map(newsDto, news);

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

