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
using webapi.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace webapi.Controllers
{
    
    public class TicketController : BaseController
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public TicketController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TicketDto>>> GetTicketsAsync([FromQuery] bool soldOut, bool completeShow)
        {
            var tickets = await unitOfWork.TicketRepository.GetTicketsAsync(soldOut, completeShow);

            if (tickets == null) return NotFound("can't find any tickets");

            return Ok(tickets);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TicketDto>> GetTicketByIdAsync(int id)
        {
            var ticket = await unitOfWork.TicketRepository.GetTicketByIdAsync(id);

            if (ticket == null) return NotFound("can't find ticket");

            return Ok(mapper.Map<TicketDto>(ticket));
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpPost]
        public async Task<ActionResult> CreateTicket(TicketDto ticketDto)
        {
            var ticket = new Ticket(ticketDto.ShowTime, ticketDto.Location, ticketDto.Price, ticketDto.Title, ticketDto.SubTitle, ticketDto.RemainNumber);

            unitOfWork.TicketRepository.CreateTicket(ticket);

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
    }
}

