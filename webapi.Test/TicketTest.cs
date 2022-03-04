using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using webapi.Controllers;
using webapi.DTOs;
using webapi.Entities;
using webapi.Interfaces;
using Xunit;
namespace webapi.Test
{
	public class TicketTest
	{
		private TicketDto GetTicket()
        {
			return new TicketDto
			{
                Id = 1,
                CityId = 1,
                CityName = "Taipei",
                ShowTime = DateTime.Parse("2022-03-18"),
                Price = 60,
                RemainNumber = 1000,
                CompleteShow = false,
                ImageUrl = "../../../assets/image/1234577.jpeg",
                Open = true,
                Title = "First Show of YUK",
                SubTitle = "Come and have fun!",
                SoldOut = false,
                PurchaseDeadLine = DateTime.Parse("2022-03-01")
			};
        }

        [Theory]
        [InlineData(1)]
		public async void Should_GetTickets_Test(int id)
        {
            var mock = new Mock<IUnitOfWork>();

            var ticketMock = new Mock<ITicketRepository>();

            ticketMock.Setup(n =>n.GetTicketByIdAsync(It.IsAny<int>()).Result).Returns(GetTicket()).Verifiable();

            mock.Setup(n => n.TicketRepository).Returns(ticketMock.Object);

            var ctrl = new TicketController(mock.Object);

            var result = await ctrl.GetTicketByIdAsync(id);

            var viewResult = result.Result;
        }
	}
}

