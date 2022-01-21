using System;
using AutoMapper;
using webapi.DTOs;
using webapi.Entities;

namespace webapi.Helper
{
    public class AutoMapHelper : Profile
    {
        public AutoMapHelper()
        {
            CreateMap<RegisterDto, AppUser>();

            CreateMap<TicketDto, Ticket>();
        }
    }
}

