using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using webapi.Extensions;
using webapi.Interfaces;

namespace webapi.Filters
{
	public class LogUserActivityFilter : IAsyncActionFilter
	{
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            ActionExecutedContext resultContext = await next();

            if (resultContext.Exception != null) return;

            if (!context.HttpContext.User.Identity.IsAuthenticated) return;

            var userId = resultContext.HttpContext.User.GetUseId();

            IUnitOfWork unitOfWork = resultContext.HttpContext.RequestServices.GetService<IUnitOfWork>();

            var user = await unitOfWork.UserRepository.GetUserByIdAsync(userId);

            user.LastLoginTime = DateTime.Now;

            await unitOfWork.Complete();
        }
    }
}

