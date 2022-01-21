using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using webapi.Data;
using webapi.Entities;
using webapi.Interfaces;

namespace webapi.Repositories
{
	public class UserRepository : IUserRepository
	{
        private readonly DataContext dataContext;

        public UserRepository(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public async Task<AppUser> GetUserByIdAsync(int userId)
        {
            return await dataContext.Users.FindAsync(userId);
        }

        public async Task<AppUser> GetUserByNameAsync(string userName)
        {
            return await dataContext.Users.SingleOrDefaultAsync(n => n.UserName == userName);
        }

        public void UpdateUser(AppUser appUser)
        {
            dataContext.Entry<AppUser>(appUser).State = EntityState.Modified;
        }
    }
}

