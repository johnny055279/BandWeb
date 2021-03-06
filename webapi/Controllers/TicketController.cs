using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using webapi.Data;
using webapi.DTOs;
using webapi.Entities;
using webapi.Extensions;
using webapi.Interfaces;

namespace webapi.Controllers
{  
    public class TicketController : BaseController
    {
        private readonly IUnitOfWork unitOfWork;

        public TicketController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TicketDto>>> GetTicketsAsync([FromQuery] bool soldOut, bool completeShow)
        {
            var tickets = await unitOfWork.TicketRepository.GetTicketsAsync(soldOut, completeShow);

            return Ok(tickets);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TicketDto>> GetTicketByIdAsync(int id)
        {
            var ticket = await unitOfWork.TicketRepository.GetTicketByIdAsync(id);

            if (ticket == null) return NotFound("can't find ticket");

            return Ok(ticket);
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpPost]
        public async Task<ActionResult> CreateTicket(TicketDto ticketDto)
        {
            unitOfWork.TicketRepository.CreateTicket(ticketDto);

            if (!await unitOfWork.Complete()) return BadRequest("Fail to create ticket");

            return Ok();
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateTicket(int id, [FromBody] TicketUpdateDto ticketUpdateDto)
        {
            await unitOfWork.TicketRepository.UpdateTicket(id, ticketUpdateDto);

            if (!await unitOfWork.Complete()) return BadRequest("Fail to update ticket");

            return NoContent();
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTicket(int id)
        {
            var ticket = await unitOfWork.TicketRepository.GetTicketByIdAsync(id);

            if (ticket.CompleteShow == true) return BadRequest("show time is complete");

            if(ticket.SoldOut == true) return BadRequest("ticket is sold out");

            await unitOfWork.TicketRepository.DeleteTicket(id);

            if (!await unitOfWork.Complete()) return BadRequest("can't delete ticket");

            return Ok();
        }

        public async Task<ActionResult> PurchaseTicket(int id, [FromQuery] int number, [FromServices] IRedisService redisService)
        {
            var user = await unitOfWork.UserRepository.GetUserByIdAsync(User.GetUserId());

            if (!await unitOfWork.TicketRepository.PurchaseTicket(id, number, user, redisService)) return BadRequest("Ticket sold out");

            if (!await unitOfWork.Complete()) return BadRequest("something went wrong");

            return Ok();
        }
    }
}

