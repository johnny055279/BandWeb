using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapi.Data;
using webapi.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace webapi.Controllers
{
    public class SystemController : BaseController
    {
        private readonly DataContext dataContext;

        private readonly IMapper mapper;

        public SystemController(DataContext dataContext, IMapper mapper)
        {
            this.dataContext = dataContext;

            this.mapper = mapper;
        }

        [HttpGet("cities")]
        public async Task<ActionResult<IEnumerable<City>>> GetCitiesAsync()
        {
            var cities = await dataContext.City.OrderBy(n => n.CityName).ToListAsync();

            return Ok(mapper.Map<IEnumerable<City>>(cities));
        }
    }
}

