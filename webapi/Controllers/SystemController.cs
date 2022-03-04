using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapi.Data;
using webapi.DTOs;
using webapi.Entities;
using webapi.Filters;
using webapi.Helper;
using webapi.Interfaces;
using webapi.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace webapi.Controllers
{
    public class SystemController : BaseController
    {
        private readonly IDropdownResult dropdownResult;

        private readonly IDropdownService dropdownService;

        public SystemController(IDropdownResult dropdownResult, IDropdownService dropdownService)
        {
            this.dropdownResult = dropdownResult;

            this.dropdownService = dropdownService;
        }

        [HttpGet("dropdown-list")]
        public async Task<ActionResult<DropDownDto>> GetDropdownListAsync([FromQuery] string type)
        {
            return Ok(await dropdownResult.GetDropdownList(dropdownService.GetCityListAsync, type));        
        }
    }
}

