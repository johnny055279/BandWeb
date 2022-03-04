using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Moq;
using webapi.Controllers;
using webapi.Data;
using webapi.Entities;
using webapi.Interfaces;
using webapi.Repositories;
using Xunit;

namespace webapi.Test
{
	public class AccountTest
	{

		[Theory]
		[InlineData("Admin")]
		public async void GetUserByNameAsyncTest(string username)
		{
			var mock = new Mock<DbSet<AppUser>>();
			mock.As<AppUser>().Setup(n => n.Id).Returns(GetSampleUser().Id);
			mock.As<AppUser>().Setup(n => n.UserName).Returns(GetSampleUser().UserName);
			mock.As<AppUser>().Setup(n => n.Email).Returns(GetSampleUser().Email);
			mock.As<AppUser>().Setup(n => n.Gender).Returns(GetSampleUser().Gender);
			mock.As<AppUser>().Setup(n => n.NickName).Returns(GetSampleUser().NickName);

			var mockContext = new Mock<DataContext>();

			mockContext.Setup(n => n.Users).Returns(mock.Object);

			var userRepository = new UserRepository(mockContext.Object);

			var user = await userRepository.GetUserByNameAsync(username);

			Assert.Equal(GetSampleUser().Id, user.Id);
			Assert.Equal(GetSampleUser().UserName, user.UserName);
			Assert.Equal(GetSampleUser().Email, user.Email);
			Assert.Equal(GetSampleUser().Gender, user.Gender);
			Assert.Equal(GetSampleUser().NickName, user.NickName);

		}

		[Theory]
		[InlineData(1)]
		public void GetUserByIdAsyncTest(int id)
        {

        }

		private AppUser GetSampleUser()
        {
			return new AppUser
			{
				Id = 1,
				UserName = "Admin",
				Email = "admin@gmail.com",
				Gender = "male",
				NickName = "admin"
            };
        }
    }
}

