using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapi.Data;
using webapi.Entities;
using webapi.Filters;
using webapi.Helper;
using webapi.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace webapi.Controllers
{
    public class SystemController : BaseController
    {
        private readonly DataContext dataContext;

        private readonly IMapper mapper;

        private readonly DropdownResult dropdownResult;

        public SystemController(DataContext dataContext, IMapper mapper, DropdownResult dropdownResult)
        {
            this.dataContext = dataContext;

            this.mapper = mapper;

            this.dropdownResult = dropdownResult;
        }

        [HttpGet("dropdown-list")]
        [ServiceFilter(typeof (DropdownDataFilter))]
        public async Task<ActionResult> GetDropdownListAsync([FromQuery] string type, [FromServices] DropdownService dropdownService)
        {
            return Ok(await dropdownResult.GetDropdownList(dropdownService.GetCityListAsync, type));
;             
        }
    }
}

