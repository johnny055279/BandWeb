using System;
using Xunit;
using Moq;
using Microsoft.EntityFrameworkCore;
using webapi.Entities;
using System.Collections.Generic;
using webapi.Data;
using webapi.Controllers;
using AutoMapper;
using webapi.Helper;
using webapi.Services;
using webapi.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace webapi.Test
{
	
	public class DropDownTest
	{ 
		[Fact]
		public async void GetCitiesTest()
		{
			var cities = new List<City>
			{
				new City{ Id = 1, CityName = "Taipei" },
				new City{ Id = 2, CityName = "Lodon" },
				new City{ Id = 3, CityName = "New York" },
				new City { Id = 4, CityName = "Berlin"}
			};

			var mockResult = new Mock<IDropdownResult>();

			var mockService = new Mock<IDropdownService>();

			mockResult.Setup(n => n.GetDropdownList(mockService.Object.GetCityListAsync, "city")).Returns(Task.FromResult(cities));

			var ctrl = new SystemController(mockResult.Object, mockService.Object);

			var result = await ctrl.GetDropdownListAsync<List<City>>("city");

			var viewResult = Assert.IsType<ActionResult<List<City>>>(result).Value;

			Assert.NotNull(viewResult);

   //         for (int i = 0; i < cities.Count; i++)
   //         {
			//	Assert.Equal(cities[i].Id, viewResult?[i].Id);

			//	Assert.Equal(cities[i].CityName, viewResult?[i].CityName);
			//}
		}
	}
}

