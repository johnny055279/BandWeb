using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using webapi.Helper;

namespace webapi.Filters
{
    public class DropdownDataFilter : IAsyncResultFilter
    {
        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            ResultExecutedContext resultExcutedContext = await next();

            if (resultExcutedContext.Exception != null) return;

            var data = resultExcutedContext.Result;

            return;
        }
    }
}

