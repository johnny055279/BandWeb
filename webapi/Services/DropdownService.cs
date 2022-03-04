using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using webapi.Data;
using webapi.DTOs;
using webapi.Entities;
using webapi.Interfaces;
using webapi.Models;

namespace webapi.Services
{
    public class DropdownService : IDropdownService
    {
        private readonly DataContext dataContext;

        public DropdownService(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public async Task<DropDownDto> GetCityListAsync()
        {
            var result = new DropDownDto()
            {
                Type = "City",
                List = new List<DropDown>()
            };

            var cities = await dataContext.City.OrderBy(n => n.CityName).ToListAsync();

            foreach (var item in cities)
            {
                result.List.Add(new DropDown(item.Id, item.CityName));
            }

            return result;
        }
    }
}

