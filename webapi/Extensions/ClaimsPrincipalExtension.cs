using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace webapi.Extensions
{
	public static class ClaimsPrincipalExtension
	{
		public static string GetUserName(this ClaimsPrincipal claimsPrincipal)
        {
			return claimsPrincipal.FindFirst(ClaimTypes.Name).Value;
        }

		public static int GetUseId(this ClaimsPrincipal claimsPrincipal)
        {
			return int.Parse(claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier).Value);
        }
	}
}

