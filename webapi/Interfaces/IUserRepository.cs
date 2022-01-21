using System;
using System.Threading.Tasks;
using webapi.Entities;

namespace webapi.Interfaces
{
	public interface IUserRepository
	{
		void UpdateUser(AppUser appUser);

		Task<AppUser> GetUserByIdAsync(int userId);

		Task<AppUser> GetUserByNameAsync(string userName);

	}
}

