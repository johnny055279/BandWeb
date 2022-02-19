using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using webapi.Data;
using webapi.Entities;

namespace webapi.Services
{
	public class DropdownService
	{
		private readonly DataContext dataContext;

		private readonly IMapper mapper;

		public DropdownService(DataContext dataContext, IMapper mapper)
		{
			this.dataContext = dataContext;

			this.mapper = mapper;
		}

		public async Task<List<City>> GetCityListAsync()
        {
			var result =  await dataContext.City.OrderBy(n => n.CityName).ToListAsync();

			return result;
		}
	}
}

