using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using webapi.Filters;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace webapi.Controllers
{
    [ServiceFilter(typeof(LogUserActivityFilter))]
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase{}
}

